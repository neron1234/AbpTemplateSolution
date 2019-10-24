using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Dto.Controllers.Auth
{
    /// <summary>
    /// User's role
    /// </summary>
    public class AuthRoleOut
    {
        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User's role
        /// </summary>
        public AuthRoleOut()
        {

        }

        /// <summary>
        /// User's role
        /// </summary>
        public AuthRoleOut(string name)
        {
            Name = name;
        }
    }
}
