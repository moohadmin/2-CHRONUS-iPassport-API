using Amazon.S3;
using FluentValidation.AspNetCore;
using iPassport.Api.AutoMapper;
using iPassport.Api.Configurations;
using iPassport.Api.Configurations.Filters;
using iPassport.Api.Models.Responses;
using iPassport.Application.Resources;
using iPassport.Domain.Entities.Authentication;
using iPassport.Infra.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.Unicode;

namespace iPassport.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private readonly IConfiguration Configuration;

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionHandlerFilterAttribute));
                options.Filters.Add(typeof(ValidateModelStateFilterAttribute));
            })
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssembly(Assembly.Load("iPassport.Api")))
            .ConfigureApiBehaviorOptions(options =>
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
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddIdentity<Users, Roles>().AddEntityFrameworkStores<PassportIdentityContext>().AddDefaultTokenProviders();

            var secret = SecretConfiguration.GetSecret();
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iPassport.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                    @"
                        JWT Authorization utilizando esquema de Bearer no header da chamada.
                        Informe 'Bearer' mais a sua chave de autorizacão no input abaixo.
                        Exemplo: 'Bearer 12345abcdef'
                    ",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        }, new List<string>()
                    }
                });
            });

            services.AddWebEncoders(o => {
                o.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
            });

            /// Update Migrations
            // services.AddHostedService<MigrationsWork>();

            ///Add DB Context
            services.AddCustomDataContext();

            ///Add Identity DB Context
            services.AddIdentityDataContext();

            ///Helth Checks
            services.AddHealthChecks();

            ///Add Dependecy Injection
            services.AddDependencyInjection();

            ///Add AutoMapper
            services.AddAutoMapperSetup();

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.AddSingleton<Resource>();

            ///Add AWS Services
            services.AddAWSService<IAmazonS3>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/api/health");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iPassport.Api v1"));

            app.UseCors(options => options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/api/health").WithMetadata(new AllowAnonymousAttribute());
                endpoints.MapControllers();
            });
        }
    }
}
