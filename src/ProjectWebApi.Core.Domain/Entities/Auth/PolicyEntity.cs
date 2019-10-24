using ProjectWebApi.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Domain.Entities.Auth
{
    /// <summary>
    /// User policy
    /// </summary>
    public class PolicyEntity : BaseEntity
    {
        /// <summary>
        /// Policy name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<PolicyClaimEntity> PolicyClaims { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<UserPolicyEntity> UserPolicies { get; set; }
    }
}
