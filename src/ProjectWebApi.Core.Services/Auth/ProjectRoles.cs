using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Services.Auth
{
    public static class ProjectRoles
    {
        /// <summary>
        /// SysAdmin
        /// </summary>
        public const string SA = "SA";       

        /// <summary>
        /// Some specialist
        /// </summary>
        public const string Specialist = "Project_Specialist";
    
        public static bool IsProjectGroup(string groupName)
        {
            return groupName.Contains("Project");
        }

        public static string GetClearName(string role)
        {
            return role.Substring(role.IndexOf("Project"));
        }
    }
}
