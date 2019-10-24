using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Dto.Controllers.Auth
{
    /// <summary>
    /// Authorized user
    /// </summary>
    public class AuthUserOut
    {
        /// <summary>
        /// Jwt
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public List<AuthRoleOut> Roles { get; set; }

        /// <summary>
        /// Policies
        /// </summary>
        public List<AuthPolicy> Policies { get; set; }

        /// <summary>
        /// Authorized user
        /// </summary>
        public AuthUserOut()
        {
            Roles = new List<AuthRoleOut>();
            Policies = new List<AuthPolicy>();
        }

        /// <summary>
        /// Authorized user
        /// </summary>
        public AuthUserOut(int id, string name, string token) : this()
        {
            Id = id;
            Name = name;
            Token = token;
        }
    }
}
