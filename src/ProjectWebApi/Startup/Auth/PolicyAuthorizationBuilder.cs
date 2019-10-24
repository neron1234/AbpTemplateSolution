using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebApi.Auth
{
    // Read https://stormpath.com/blog/tutorial-policy-based-authorization-asp-net-core

    /// <summary>
    /// Policies
    /// </summary>
    public static class PolicyAuthorizationBuilder
    {
        /// <summary>
        /// Adds policies
        /// </summary>
        public static IServiceCollection AddProjectAuthorization(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                // simple test
                options.AddPolicy("TestPolicy", policy => policy.RequireClaim("FooBar").RequireClaim("TestClaim"));

                // custom policy
                // options.AddPolicy("FooBar", policy => policy.RequireAssertion(context => context.User.HasClaim(w => w.Type == "TestClaim")));
            });
        }
    }
}
