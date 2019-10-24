using Abp.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Utils
{
    public class ProjectSettingProvider : SettingProvider
    {
        private readonly IConfiguration _configuration;

        public ProjectSettingProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return GetSettingDefinitions(_configuration.GetChildren());
        }

        private IEnumerable<SettingDefinition> GetSettingDefinitions(IEnumerable<IConfigurationSection> sections)
        {
            if (sections == null)
            {
                throw new ArgumentNullException(nameof(sections));
            }

            foreach (var section in sections)
            {
                if (string.IsNullOrEmpty(section.Value) == false)
                {
                    yield return new SettingDefinition(section.Path.Replace(':', '.'), section.Value);
                }

                foreach (var settingDefinition in GetSettingDefinitions(section.GetChildren()))
                {
                    yield return settingDefinition;
                }
            }
        }

    }
}
