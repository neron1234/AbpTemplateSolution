using Ganss.XSS;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjectWebApi.Sanitizer
{
    internal class SanitizerValueProvider : IValueProvider
    {
        private readonly PropertyInfo _targetProperty;
        private readonly HtmlSanitizer _htmlSanitizer;

        public SanitizerValueProvider(PropertyInfo targetProperty)
        {
            _targetProperty = targetProperty;
            _htmlSanitizer = new HtmlSanitizer();
        }

        public void SetValue(object target, object value)
        {
            if (value is string s)
            {
                var pureStr = _htmlSanitizer.Sanitize(s);
                _targetProperty.SetValue(target, pureStr);
            }
        }

        public object GetValue(object target)
        {
            return _targetProperty.GetValue(target);
        }
    }
}
