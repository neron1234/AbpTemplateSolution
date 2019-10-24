using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace ProjectWebApi.Core.EF
{
    [DependsOn(
        typeof(ProjectCoreModule),
        typeof(AbpEntityFrameworkCoreModule))]
    public class ProjectCoreEfModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectCoreEfModule).GetAssembly());
        }
    }
}