using ProjectWebApi.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Domain.Entities.Auth
{
    /// <summary>
    /// PIVOT between <see cref="UserEntity"/> and <see cref="PolicyEntity"/>
    /// </summary>
    public class UserPolicyEntity : BaseEntity
    {
        public UserEntity User { get; set; }
        public int UserId { get; set; }

        public PolicyEntity Policy { get; set; }
        public int PolicyId { get; set; }
    }
}
