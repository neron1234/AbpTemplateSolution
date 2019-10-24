using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace ProjectWebApi.Core.Services.Auth
{
    public class ADService
    {
        public IEnumerable<string> GetGroupNames(WindowsIdentity identity)
        {
            return from translatedGroup in identity.Groups.Select(s => s.Translate(typeof(NTAccount)))
                   where ProjectRoles.IsProjectGroup(translatedGroup.Value)
                   select ProjectRoles.GetClearName(translatedGroup.Value);
        }

        //public IEnumerable<GroupPrincipal> GetGroups(string userName)
        //{
        //    var yourDomain = new PrincipalContext(ContextType.Domain);
        //    var user = UserPrincipal.FindByIdentity(yourDomain, userName);
        //    if (user == null)
        //    {
        //        throw new Exception($"User '{userName}' not found.");
        //    }

        //    var groups = user.GetAuthorizationGroups().Where(w => ProjectRoles.IsProjectGroup(w.Name)).ToArray();
        //    foreach (var group in groups.OfType<GroupPrincipal>())
        //    {
        //        yield return group;
        //    }
        //}

        public IEnumerable<ADUserInfo> GetUsers(string groupName)
        {
            using (var domain = new PrincipalContext(ContextType.Domain))
            using (var group = GroupPrincipal.FindByIdentity(domain, IdentityType.SamAccountName, groupName))
            {
                if (group == null)
                {
                    throw new Exception($"Group '{groupName}' not found.");
                }

                foreach (var user in group.Members.OfType<UserPrincipal>())
                {
                    using (user)
                    {
                        yield return new ADUserInfo(user.Sid.Value, user.Name, user.EmailAddress);
                    }
                }
            }
        }

        public ADUserInfo GetUser(string sid)
        {
            using (var domain = new PrincipalContext(ContextType.Domain))
            using (var user = UserPrincipal.FindByIdentity(domain, IdentityType.Sid, sid))
            {
                return new ADUserInfo(user.Sid.Value, user.Name, user.EmailAddress);
            }
        }
    }
}
