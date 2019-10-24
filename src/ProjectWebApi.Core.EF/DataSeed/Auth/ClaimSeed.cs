using ProjectWebApi.Core.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.EF.DataSeed
{
    public static class ClaimSeed
    {
        public static IEnumerable<ClaimEntity> AddClaims(this ModelBuilder modelBuilder)
        {
            int id = 1;

            var claims = new[] 
            {
                GetClaim(id++, "FooBar"),
                GetClaim(id++, "TestClaim")
            };

            modelBuilder.Entity<ClaimEntity>(m => m.HasData(claims));
            modelBuilder.IdStartAt<ClaimEntity>(id);

            return claims;
        }

        private static ClaimEntity GetClaim(int id, string name)
        {
            return new ClaimEntity
            {
                Id = id,
                Name = name,
                CreationTime = SeedFather.CreationTime
            };
        }
    }
}
