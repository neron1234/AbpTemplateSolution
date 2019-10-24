using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProjectWebApi.Core.Services.Auth
{
    public class AuthOptions
    {
        private const string KEY = "-D``bBA@HxaHK-V'U3}~U5[!y8d>TY`inggggdfdse9CoQOdJ4(xx:kSm)o";

        /// <summary>
        /// Token lifetime, minutes
        /// </summary>
        public const int LIFETIME = 60;

        /// <summary>
        /// Publisher
        /// </summary>
        public const string ISSUER = "Kovtun";

        /// <summary>
        /// Consumer
        /// </summary>
        public const string AUDIENCE = "Wohoo";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
