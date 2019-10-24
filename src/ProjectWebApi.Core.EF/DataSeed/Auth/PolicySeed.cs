using ProjectWebApi.Core.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.EF.DataSeed
{
    public static class PolicySeed
    {
        public static IEnumerable<PolicyEntity> AddPolicies(this ModelBuilder modelBuilder)
        {
            int id = 1;

            var policies = new[]
            {
                GetPolicy(id++, "TestPolicy"),
            };

            modelBuilder.Entity<PolicyEntity>(m => m.HasData(policies));
            modelBuilder.IdStartAt<PolicyEntity>(id);

            return policies;
        }

        private static PolicyEntity GetPolicy(int id, string name)
        {
            return new PolicyEntity
            {
                Id = id,                
                Name = name,
                CreationTime = SeedFather.CreationTime
            };
        }
    }
}
