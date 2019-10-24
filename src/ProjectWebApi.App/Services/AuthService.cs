using ProjectWebApi.Core.Dto.Controllers.Auth;
using ProjectWebApi.Core.Services.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Linq;
using Abp.UI;
using ProjectWebApi.Core.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using ProjectWebApi.Core.Dto.Entities;
using ProjectWebApi.Core.Dto.Entities.Auth;

namespace ProjectWebApi.App.Services
{
    public class AuthService : AppServiceBase
    {
        private readonly ADService _Ad;

        public AuthService()
        {
            _Ad = new ADService();
        }

        #region GetJwtFromWindowsAsync

        public async Task<AuthUserOut> GetJwtFromWindowsAsync(WindowsIdentity identity)
        {
            var dbUser = await GetUserFromDbAsync(identity.User.Value);
            
            // Token
            var jwtIdentity = GetJwtIdentity(identity, dbUser);
            var jwt = GetJwt(jwtIdentity);

            // User
            var userDto = new AuthUserOut(dbUser.Id, identity.Name, jwt);

            // Roles
            var groupsDto = _Ad.GetGroupNames(identity).Select(s => new AuthRoleOut(s));
            userDto.Roles.AddRange(groupsDto);

            // Policies
            var policiesDto = from policy in dbUser.UserPolicies.Select(s => s.Policy)
                              select new AuthPolicy(policy.Name);
            userDto.Policies.AddRange(policiesDto);

            return userDto;
        }

        private async Task<UserEntity> GetUserFromDbAsync(string sid)
        {
            var dbUser = await GetUsers().FirstOrDefaultAsync(f => f.Sid == sid);

            if (dbUser is null)
            {
                // Add user
                var userPrinc = _Ad.GetUser(sid);
                dbUser = CreateUser(userPrinc.Name, sid);
                await GetRepo<UserEntity>().InsertAndGetIdAsync(dbUser);

                // Add policies
            }

            return dbUser;
        }

        private UserEntity CreateUser(string name, string sid)
        {
            return new UserEntity
            {
                Name = name,
                Sid = sid,
                UserPolicies = new List<UserPolicyEntity>()
            };
        }

        private ClaimsIdentity GetJwtIdentity(WindowsIdentity identity, UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, identity.Name),
                new Claim("UserId", user.Id.ToString()),
            };

            // Adds policies claims
            foreach (var dbClaim in from policy in user.UserPolicies.Select(s => s.Policy)
                                    from claim in policy.PolicyClaims.Select(s => s.Claim)
                                    select claim)
            {
                claims.Add(new Claim(dbClaim.Name, string.Empty));
            }

            // Add roles
            foreach (var group in _Ad.GetGroupNames(identity))
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, group));
            }

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private string GetJwt(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        #endregion

        public async Task<IList<UserDto>> GetUsersForGroupAsync(string groupName)
        {
            var usersDto = new List<UserDto>();

            var usersPricipals = _Ad.GetUsers(groupName);
            foreach (var userPri in usersPricipals)
            {
                var dbUser = await GetUserFromDbAsync(userPri.Sid);
                var userDto = Map(dbUser);
                usersDto.Add(userDto);
            }

            return usersDto;
        }
    }
}
