using ProjectWebApi.Core.Services.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebApi.Auth
{
    /// <summary>
    /// Jwt authentication
    /// </summary>
    public static class JwtAuthenticationBuilder
    {
        /// <summary>
        /// Adds Jwt authentication
        /// </summary>
        public static AuthenticationBuilder AddJwtProjectAuthentication(this IServiceCollection services)
        {
            return services
                    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                     {
                         options.RequireHttpsMetadata = false;
                         options.SaveToken = true;
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,
                             ValidIssuer = AuthOptions.ISSUER,

                             ValidateAudience = true,
                             ValidAudience = AuthOptions.AUDIENCE,

                             ValidateLifetime = true,

                             IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                             ValidateIssuerSigningKey = true,
                         };
                     });
        }
    }
}
