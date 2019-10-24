using ProjectWebApi.Core.Dto.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectWebApi.Core.EF;
using Abp.UI;

namespace ProjectWebApi.App.Services
{
    public class DictionaryService : AppServiceBase
    {
        public async Task<TestModelDto[]> GetTestModelsAsync()
        {
            var services = await GetServices().Select(q => Map(q)).ToArrayAsync();
            return services;
        }     
    }
}
