using ProjectWebApi.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Domain.Entities.Auth
{
    /// <summary>
    /// PIVOT between <see cref="PolicyEntity"/> and <see cref="ClaimEntity"/>
    /// </summary>
    public class PolicyClaimEntity : BaseEntity
    {
        public PolicyEntity Policy { get; set; }
        public int PolicyId { get; set; }

        public ClaimEntity Claim { get; set; }
        public int ClaimId { get; set; }
    }
}
