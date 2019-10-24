using Abp.AspNetCore;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjectWebApi.App;
using ProjectWebApi.Core;
using ProjectWebApi.Core.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ProjectWebApi
{
    /// <summary>
    /// Web module
    /// </summary>
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(ProjectCoreEfModule),
        typeof(ProjectAppModule)
    )]
    public class ProjectWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        ///  Web module
        /// </summary>
        public ProjectWebModule(IWebHostEnvironment env)
        {
            _appConfiguration = AppConfiguration.Get(env.ContentRootPath, env.EnvironmentName);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString("DefaultConnection");

            // should be set false after debugging
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectWebModule).GetAssembly());
        }
    }
}
