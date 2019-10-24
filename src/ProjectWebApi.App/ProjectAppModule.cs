using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ProjectWebApi.Core;

namespace ProjectWebApi.App
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(ProjectCoreModule)
)]
    public class ProjectAppModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg => cfg.AddMaps(typeof(ProjectAppModule)));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectAppModule).GetAssembly());
        }
    }
}
