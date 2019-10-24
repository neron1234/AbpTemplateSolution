using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Dto.Controllers.Auth
{
    /// <summary>
    /// User policies
    /// </summary>
    public class AuthPolicy
    {
        /// <summary>
        /// Policy name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User policies
        /// </summary>
        public AuthPolicy()
        {

        }

        /// <summary>
        /// User policies
        /// </summary>
        public AuthPolicy(string name)
        {
            Name = name;
        }
    }
}
