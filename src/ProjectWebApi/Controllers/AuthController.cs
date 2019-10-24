using Abp.AspNetCore.Mvc.Controllers;
using ProjectWebApi.App.Services;
using ProjectWebApi.Core.Services.Auth;
using ProjectWebApi.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ProjectWebApi.Controllers
{
    /// <summary>
    /// Auth
    /// </summary>
    [Route("api/[controller]")]
    public class AuthController : ProjectController
    {
        private readonly AuthService _authService;

        /// <summary>
        /// Auth
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(AuthService authService)
        {
            _authService = SetOrThrowException(() => authService);
        }

        /// <summary>
        /// Windows auth, returns JWT
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        [Authorize(AuthenticationSchemes = "Windows")]
        public async Task<ObjectResult> Login()
        {
            var windowsIdentity = (WindowsIdentity)User.Identity;

            var res = await _authService.GetJwtFromWindowsAsync(windowsIdentity);
            return Ok(res);
        }

  
        /// <summary>
        /// Returns users from group "Specialist"
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUsersForSpecialist")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ObjectResult> GetUsersFromRoleSpecialist()
        {
            var res = await _authService.GetUsersForGroupAsync(ProjectRoles.Specialist);
            return Ok(res);
        }     
    }
}
