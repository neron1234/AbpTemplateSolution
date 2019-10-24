using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectWebApi.Core.Domain.Entities.Auth;
using ProjectWebApi.Core.Domain.Entities.Common;
using ProjectWebApi.Core.Domain.Entities.Dictionaries;
using ProjectWebApi.Core.Dto.Entities;
using ProjectWebApi.Core.Dto.Entities.Auth;
using ProjectWebApi.Core.EF;
using ProjectWebApi.Core.Services.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebApi.App
{
    public class AppServiceBase : ApplicationService
    {
        protected readonly IocManager _ioc;
        protected readonly EmailSender _emailSender;
        protected readonly IDbContextProvider<ProjectDbContext> _dbContextProvider;

        public AppServiceBase()
        {
            _ioc = IocManager.Instance;
            _emailSender = _ioc.Resolve<EmailSender>();
            _repositories = new Dictionary<Type, IRepository>();
            _dbContextProvider = _ioc.Resolve<IDbContextProvider<ProjectDbContext>>();
        }

        #region Repositories

        #region UserEntity

        protected virtual IQueryable<UserEntity> GetUsers()
        {
            return GetAll<UserEntity>()
                .Include(q => q.UserPolicies).ThenInclude(q => q.Policy);
        }

        #endregion

        #region ClaimEntity

        protected virtual IQueryable<ClaimEntity> GetClaims()
        {
            return GetAll<ClaimEntity>();
        }

        #endregion

        #region PolicyEntity

        protected virtual IQueryable<PolicyEntity> GetPolicies()
        {
            return GetAll<PolicyEntity>()
                .Include(q => q.PolicyClaims).ThenInclude(q => q.Claim);
        }

        #endregion


        #region TestModelEntity

        protected virtual IQueryable<TestModelEntity> GetServices()
        {
            return GetAll<TestModelEntity>();
        }

        #endregion   
  
        #endregion

        #region Mapping
     
        protected TestModelDto Map(TestModelEntity source)
        {
            return Map<TestModelDto>(source);
        }

        protected UserDto Map(UserEntity source)
        {
            return Map<UserDto>(source);
        }

        protected T Map<T>(object source)
        {
            return ObjectMapper.Map<T>(source);
        }

        #endregion

        #region Bulk

        protected async Task BulkInsertAsync<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            if (entities.Any())
            {
                PrepareForInsert(entities);

                var dbContext = _dbContextProvider.GetDbContext();
                await dbContext.BulkInsertAsync(entities);
            }
        }

        protected void BulkInsert<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            if (entities.Any())
            {
                PrepareForInsert(entities);

                var dbContext = _dbContextProvider.GetDbContext();
                dbContext.BulkInsert(entities);
            }
        }

        protected async Task BulkUpdateAsync<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            if (entities.Any())
            {
                PrepareForUpdate(entities);

                var dbContext = _dbContextProvider.GetDbContext();
                await dbContext.BulkUpdateAsync(entities);
            }
        }

        protected void BulkUpdate<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            if (entities.Any())
            {
                PrepareForUpdate(entities);

                var dbContext = _dbContextProvider.GetDbContext();
                dbContext.BulkUpdate(entities);
            }
        }

        private void PrepareForUpdate<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            // Custom update, because it's not Abp
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.LastModificationTime = now;
            }
        }

        private void PrepareForInsert<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            // Custom update, because it's not Abp
            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.CreationTime = now;
            }
        }

        protected void BulkDelete<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            if (entities.Any())
            {
                var dbContext = _dbContextProvider.GetDbContext();
                dbContext.RemoveRange(entities);
            }
        }

        #endregion

        #region Utils

        /// <summary>
        /// Force save to database (commits new UOW)
        /// </summary>
        protected async Task Force(Func<Task> taskForForceUpdate)
        {
            using (var uow = UnitOfWorkManager.Begin(System.Transactions.TransactionScopeOption.RequiresNew))
            {
                await taskForForceUpdate();
                uow.Complete();
            }
        }    

        #endregion

        #region Register Repo

        protected Dictionary<Type, IRepository> _repositories;

        protected IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : BaseEntity
        {
            return GetRepo<TEntity>().GetAll();
        }

        protected IRepository<TEntity> GetRepo<TEntity>()
            where TEntity : BaseEntity
        {
            var entityType = typeof(TEntity);

            if (!_repositories.TryGetValue(entityType, out var repo))
            {
                var repoType = typeof(IRepository<TEntity>);
                repo = (IRepository)_ioc.Resolve(repoType);

                _repositories.Add(entityType, repo);
            }

            return (IRepository<TEntity>)repo;
        }

        #endregion
    }
}
