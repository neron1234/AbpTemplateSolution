using Abp.AspNetCore.Mvc.Controllers;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjectWebApi.Controllers
{
    /// <summary>
    /// Base project controller
    /// </summary>
    public abstract class ProjectController : AbpController
    {
        /// <summary>
        /// Current user id
        /// </summary>
        protected int UserId
        {
            get => GetUserId();
        }

        private int GetUserId()
        {
            var idClaim = User.Claims.FirstOrDefault(f => f.Type == "UserId");

            if(idClaim is null)
            {
                throw new UserFriendlyException("Claim 'UserId' not found.");
            }

            return int.Parse(idClaim.Value);
        }

        /// <summary>
        /// Set value or throws <see cref="ArgumentNullException"/>
        /// </summary>
        protected T SetOrThrowException<T>(Func<T> valueFunc)
            where T : class
        {
            var value = valueFunc();

            if (value is null)
            {
                var name = valueFunc.Method.ReflectedType.GetRuntimeFields().First().Name;
                throw new ArgumentNullException(name);
            }

            return value;
        }
    }
}
