using ProjectWebApi.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Domain.Entities.Auth
{
    /// <summary>
    /// User claim
    /// </summary>
    public class ClaimEntity : BaseEntity
    {
        /// <summary>
        /// Claim name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<PolicyClaimEntity> PolicyClaims { get; set; }
    }
}
