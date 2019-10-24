using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProjectWebApi.Core.Dto.Entities.Auth;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebApi
{
    /// <summary>
    /// Swagger for project
    /// </summary>
    public static class ProjectSwagger
    {
        /// <summary>
        /// Adds project swagger
        /// </summary>
        public static IServiceCollection AddProjectSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Send description to front with love :)
                options.IncludeXmlComments(Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml"));
                options.IncludeXmlComments(Path.ChangeExtension(typeof(UserDto).Assembly.Location, ".xml"));

                // Add jwt auth to swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Fill input: bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                var oaReference = new OpenApiReference()
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                };

                var securityScheme = new OpenApiSecurityScheme { Reference = oaReference };
                var security = new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                };
                options.AddSecurityRequirement(security);
            });
        }
    }
}
