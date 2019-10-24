using Abp.Domain.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.Bulk;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;

namespace ProjectWebApi.Core.EF
{
    public static class EFExtensions
    {
        public static async Task BulkInsertAsync<TEntity>(this DbContext dbContext, IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            var bulk = new NpgsqlBulkUploader(dbContext);
            await bulk.InsertAsync(entities);
        }

        public static void BulkInsert<TEntity>(this DbContext dbContext, IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            var bulk = new NpgsqlBulkUploader(dbContext);
            bulk.Insert(entities);
        }

        public static async Task BulkUpdateAsync<TEntity>(this DbContext dbContext, IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            var bulk = new NpgsqlBulkUploader(dbContext);
            await bulk.UpdateAsync(entities);
        }

        public static void BulkUpdate<TEntity>(this DbContext dbContext, IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            var bulk = new NpgsqlBulkUploader(dbContext);
            bulk.Update(entities);
        }

        /// <summary>
        /// Adds database sequence
        /// </summary>
        public static void IdStartAt<TEntity>(this ModelBuilder modelBuilder, int idStartValue)
            where TEntity : Entity
        {
            var seqName = $"{typeof(TEntity).Name}_seq";

            modelBuilder.HasSequence<int>(seqName)
                .StartsAt(idStartValue)
                .IncrementsBy(1);

            modelBuilder.Entity<TEntity>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"nextval('\"{seqName}\"')");
        }

        #region Repository

        /// <summary>
        /// Returns filtered query if needFilter is true
        /// </summary>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, bool needFilter, Expression<Func<TSource, bool>> predicate)
        {
            return needFilter ? source.Where(predicate) : source;
        }

        #endregion
    }
}
