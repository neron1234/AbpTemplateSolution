using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using System.Buffers;

namespace ProjectWebApi.Sanitizer
{
    internal static class SanitizerExtensions
    {
        public static void UseSanitizerInputFormatter(this MvcOptions opts, IServiceCollection services)
        {
            opts.InputFormatters.RemoveType<NewtonsoftJsonInputFormatter>();

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new SanitizerContractResolver()
            };

            var sp = services.BuildServiceProvider();
            var logger = sp.GetService<ILoggerFactory>().CreateLogger<MvcOptions>();
            var objectPoolProvider = sp.GetService<ObjectPoolProvider>();
            var jsonOpt = new MvcNewtonsoftJsonOptions();

            var jsonInputFormatter = new NewtonsoftJsonInputFormatter(logger, serializerSettings, ArrayPool<char>.Shared, objectPoolProvider, opts, jsonOpt);
            opts.InputFormatters.Add(jsonInputFormatter);
        }
    }
}
