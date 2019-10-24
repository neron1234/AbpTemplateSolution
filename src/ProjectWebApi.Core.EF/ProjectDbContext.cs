using Abp.EntityFrameworkCore;
using ProjectWebApi.Core.Domain.Entities;
using ProjectWebApi.Core.Domain.Entities.Auth;
using ProjectWebApi.Core.Domain.Entities.Dictionaries;
using ProjectWebApi.Core.EF.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace ProjectWebApi.Core.EF
{
    public class ProjectDbContext : AbpDbContext
    {

        #region Auth

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ClaimEntity> Claims { get; set; }
        public DbSet<PolicyEntity> Policies { get; set; }
        public DbSet<PolicyClaimEntity> PolicyClaims { get; set; }
        public DbSet<UserPolicyEntity> UserPolicies { get; set; }

        #endregion

        #region Dictionaries

        public DbSet<TestModelEntity> Models { get; set; }

        #endregion

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var policies = modelBuilder.AddPolicies();
            var claims = modelBuilder.AddClaims();
            modelBuilder.AddPolicyClaims(policies, claims);

            var services = modelBuilder.AddTestModels();
        }
    }
}