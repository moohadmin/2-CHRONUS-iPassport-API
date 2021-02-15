using iPassport.Api.AutoMapper;
using iPassport.Api.Configurations;
using iPassport.Api.Configurations.Filters;
using iPassport.Api.Models.Responses;
using iPassport.Application.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace iPassport.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                options.Filters.Add(typeof(ExceptionHandlerFilterAttribute))).ConfigureApiBehaviorOptions(options => 
                {
                    options.InvalidModelStateResponseFactory = c =>
                    {
                        var resource = c.HttpContext.RequestServices.GetRequiredService<Resource>();
                        var errorMessage = resource.GetMessage("InvalidJson");

                        return new BadRequestObjectResult(new ServerErrorResponse
                                    (
                                        errorMessage,
                                        null,
                                        null
                                    ));
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iPassport.Api", Version = "v1" });
            });

            ///Add DB Context
            services.AddCustomDataContext(Configuration);

            ///Helth Checks
            services.AddHealthChecks();

            ///Add Dependecy Injection
            services.AddDependencyInjection();

            ///Add AutoMapper
            services.AddAutoMapperSetup();

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.AddSingleton<Resource>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iPassport.Api v1"));
            }

            app.UseCors(options => options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
                       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/api/health");
            });
        }
    }
}
