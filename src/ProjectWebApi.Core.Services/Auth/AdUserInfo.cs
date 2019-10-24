using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Services.Auth
{
    /// <summary>
    /// User info from ActiveDirectory
    /// </summary>
    public class ADUserInfo
    {
        public string Sid { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public ADUserInfo()
        {

        }

        public ADUserInfo(string sid, string name, string email)
        {
            Sid = sid;
            Name = name;
            Email = email;
        }
    }
}
