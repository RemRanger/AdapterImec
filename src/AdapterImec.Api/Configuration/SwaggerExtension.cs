using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace AdapterImec.Api.Configuration
{
    /// <summary>
    /// Configuation Extension for Swagger 
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Use Custom Swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseCustomSwagger(this IApplicationBuilder app, string usePathBase)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"{usePathBase}/swagger/v1/swagger.json", "AdapterImec.Api v1"));
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "AdapterImec.Api v1"));
        }

        /// <summary>
        /// Add Swagger
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services, string tokenUrl)
        {
            const string authorizationHeaderName = "Authorization";
            const string schemaName = "Datahub";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdapterImec.Api", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = authorizationHeaderName,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{tokenUrl}/protocol/openid-connect/token")
                        }
                    }
                };

                c.AddSecurityDefinition(schemaName, securitySchema);

                var requirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = schemaName
                            },
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            Name = authorizationHeaderName,
                            Type = SecuritySchemeType.OAuth2
                        },
                        new List<string>()
                    }
                };

                c.AddSecurityRequirement(requirement);

            });
        }
    }
}
