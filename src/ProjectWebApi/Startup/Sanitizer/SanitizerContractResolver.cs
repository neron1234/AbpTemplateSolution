using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebApi.Sanitizer
{
    internal class SanitizerContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            foreach (var property in properties.Where(p => p.PropertyType == typeof(string)))
            {
                var propertyInfo = type.GetProperty(property.UnderlyingName);
                if (propertyInfo != null)
                {
                    property.ValueProvider = new SanitizerValueProvider(propertyInfo);
                }
            }

            return properties;
        }
    }
}
