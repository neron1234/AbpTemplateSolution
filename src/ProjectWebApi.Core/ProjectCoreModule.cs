using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.MailKit;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Reflection.Extensions;
using AutoMapper;
using Castle.MicroKernel.Registration;
using ProjectWebApi.Core.Dto.Entities;
using ProjectWebApi.Core.Services.Email;
using ProjectWebApi.Core.Services.Email.ExchangeServer;
using ProjectWebApi.Core.Services.Email.MessageCreators;
using ProjectWebApi.Core.Services.Email.MessageCreators.Common;
using ProjectWebApi.Core.Services.Email.Smtp;
using ProjectWebApi.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProjectWebApi.Core
{
    [DependsOn(typeof(AbpMailKitModule), typeof(AbpAutoMapperModule))]
    public class ProjectCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Add settings from appsettings.json
            Configuration.Settings.Providers.Add<ProjectSettingProvider>();

            // Email
            RegisterEmailSender();

            // Dynamic AutoMapper for entities
            Configuration.Modules.AbpAutoMapper().Configurators.Add(ConfugureEntitiesMapper);
        }

        private void RegisterEmailSender()
        {
            // Smtp client from Abp
            IocManager.Register<IEmailSender, SmtpSender>(DependencyLifeStyle.Transient);

            // ExchangeServer, see settings in ExchangeServerSender
            // IocManager.Register<IEmailSender, ExchangeServerSender>(DependencyLifeStyle.Transient);

            // Email sender service
            IocManager.IocContainer.Register(Component
                .For<EmailSender>()
                .ImplementedBy<EmailSender>()
                .LifestyleTransient()
                .OnCreate(ConfigureEmailSender));
        }

        private void ConfigureEmailSender(EmailSender sender)
        {
            var emailConfirmation = new EmailConfirmationCreator();
            var notifyAfterEmailConfirmation = new NotifyAfterEmailConfirmationCreator();

            sender.RegisterMessageCreator(emailConfirmation);
            sender.RegisterMessageCreator(notifyAfterEmailConfirmation);
        }

        private void ConfugureEntitiesMapper(IMapperConfigurationExpression config)
        {
            foreach (var mappingPair in GetMappingPairs())
            {
                config.CreateMap(mappingPair.typeFrom, mappingPair.typeTo);
            }
        }

        private IEnumerable<(Type typeFrom, Type typeTo)> GetMappingPairs()
        {
            var mappingTypes = GetMappingTypes();

            foreach (var typeTo in mappingTypes)
            {
                var typeFrom = typeTo.GetCustomAttribute<AutoMapFromAttribute>().TargetTypes[0];

                yield return (typeFrom, typeTo);
                yield return (typeTo, typeFrom);
            }
        }

        private Type[] GetMappingTypes()
        {
            var baseMappingType = typeof(EntityDto);

            var assembly = Assembly.GetAssembly(typeof(TestModelDto));
            var mappingTypes = assembly.GetTypes().Where(w => baseMappingType.IsAssignableFrom(w)).ToArray();

            return mappingTypes;
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectCoreModule).GetAssembly());
        }
    }
}
