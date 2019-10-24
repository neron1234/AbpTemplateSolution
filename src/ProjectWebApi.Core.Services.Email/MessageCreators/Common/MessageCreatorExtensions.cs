using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Services.Email.MessageCreators.Common
{
    public static class MessageCreatorExtensions
    {
        public static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
