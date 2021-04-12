using iPassport.Api.Models.Responses;
using iPassport.Application.Exceptions;
using iPassport.Application.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Net;

namespace iPassport.Api.Configurations.Filters
{
    /// Filter Exception Attribute
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        private readonly Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        private readonly IStringLocalizer<Resource> _localizer;
        
        private readonly bool _devEnv;
        
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="env">Current Web Host Environment</param>
        /// <param name="localizer">Localizer</param>
        public ExceptionHandlerFilterAttribute(IWebHostEnvironment env, IStringLocalizer<Resource> localizer) : base()
        {
            _devEnv = env.IsDevelopment();
            _localizer = localizer;
        }

        /// <summary>
        /// Filter exception
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessException)
            {
                var statusCode = (context.Exception as BusinessException).HttpCode;

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = statusCode;
                context.Result = new JsonResult(new BussinessExceptionResponse
                (
                    new List<string>() { context.Exception.Message }
                ));

                LogError(context.Exception);
                return;
            }

            if (context.Exception is UniqueKeyException)
            {
                var statusCode = (context.Exception as UniqueKeyException).HttpCode;

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = statusCode;
                context.Result = new JsonResult(new BussinessExceptionResponse
                (
                    new List<string>() { string.Format(_localizer["DataAlreadyRegistered"], ((UniqueKeyException)context.Exception).Key) }
                ));

                LogError(context.Exception);
                return;
            }

            if (context.Exception is NotFoundException)
            {
                var statusCode = (context.Exception as NotFoundException).HttpCode;

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = statusCode;
                context.Result = new JsonResult(new BussinessExceptionResponse
                (
                    context.Exception.Message
                ));

                LogError(context.Exception);
                return;
            }

            context.HttpContext.Response.ContentType = "application/json";

            if (context.Exception is PersistenceException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Result = new JsonResult(new ServerErrorResponse
                (
                    context.Exception.Message,
                    _devEnv ? context.Exception.InnerException?.Message : null,
                    _devEnv ? context.Exception.StackTrace : null
                )); ;

                LogError(context.Exception);
                return;
            }

            if (context.Exception is InternalErrorException)
            {
                context.HttpContext.Response.StatusCode = (context.Exception as InternalErrorException).HttpCode;

                context.Result = new JsonResult(new ServerErrorResponse
                (
                    context.Exception.Message,
                    _devEnv ? context.Exception.InnerException?.Message : null,
                    _devEnv ? (context.Exception as InternalErrorException).Trace : null
                ));

                LogError(context.Exception);
                return;
            }

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Result = new JsonResult(new ServerErrorResponse
            (
                context.Exception.Message,
                _devEnv ? context.Exception.InnerException?.Message : null,
                _devEnv ? context.Exception.StackTrace : null
            ));

            LogError(context.Exception);
        }

        private void LogError(Exception exception)
        {
            logger.Error(exception, exception.Message);
            if (exception.InnerException != null)
            {
                logger.Error(exception, exception.InnerException.ToString());
            }
            logger.Error(exception, exception.StackTrace);
        }
    }
}
