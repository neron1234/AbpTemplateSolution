using ProjectWebApi.Core.Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.EF.DataSeed
{
    public static class SeedTestModel
    {
        public static IEnumerable<TestModelEntity> AddTestModels(this ModelBuilder modelBuilder)
        {
            int id = 1;

            var res = new[]
            {
                Get(id++, "First"),
                Get(id++, "Second"),
                Get(id++, "Pony")
            };

            modelBuilder.Entity<TestModelEntity>(m => m.HasData(res));
            modelBuilder.IdStartAt<TestModelEntity>(id);

            return res;
        }

        private static TestModelEntity Get(int id, string name)
        {
            return new TestModelEntity
            {
                Id = id,
                Name = name,
                CreationTime = SeedFather.CreationTime
            };
        }
    }
}
