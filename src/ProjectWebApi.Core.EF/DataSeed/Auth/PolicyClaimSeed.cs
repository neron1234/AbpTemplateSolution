using ProjectWebApi.Core.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectWebApi.Core.EF.DataSeed
{
    public static class PolicyClaimSeed
    {
        public static void AddPolicyClaims(this ModelBuilder modelBuilder, 
            IEnumerable<PolicyEntity> policies, IEnumerable<ClaimEntity> claims)
        {
            int id = 1;

            var policyClaims = new List<PolicyClaimEntity>();
            policyClaims.AddRange(GetForTest(ref id, claims, policies));

            modelBuilder.Entity<PolicyClaimEntity>(m => m.HasData(policyClaims));
            modelBuilder.IdStartAt<PolicyClaimEntity>(id);
        }

        private static IEnumerable<PolicyClaimEntity> GetForTest(ref int id, IEnumerable<ClaimEntity> claims, IEnumerable<PolicyEntity> policies)
        {
            var res = new List<PolicyClaimEntity>();
            var testPolicy = policies.First(f => f.Name == "TestPolicy");

            foreach (var testClaim in claims.Where(w => w.Name == "FooBar" || w.Name == "TestClaim"))
            {
                var item = GetPolicyClaim(id++, testPolicy.Id, testClaim.Id);
                res.Add(item);
            }

            return res;
        }

        private static PolicyClaimEntity GetPolicyClaim(int id, int policyId, int claimId)
        {
            return new PolicyClaimEntity
            {
                Id = id,
                PolicyId = policyId,
                ClaimId = claimId,
                CreationTime = SeedFather.CreationTime
            };
        }
    }
}
