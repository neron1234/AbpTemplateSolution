using ProjectWebApi.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Domain.Entities.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// SID windows user
        /// </summary>
        public string Sid { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<UserPolicyEntity> UserPolicies { get; set; }
    }
}
