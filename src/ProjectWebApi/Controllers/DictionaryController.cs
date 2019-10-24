using Abp.AspNetCore.Mvc.Controllers;
using ProjectWebApi.App.Services;
using ProjectWebApi.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWebApi.Core.Dto.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProjectWebApi.Controllers
{
    /// <summary>
    /// Dictionary
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DictionaryController : ProjectController
    {
        private readonly DictionaryService _dictionaryService;

        /// <summary>
        /// Dictionary
        /// </summary>
        public DictionaryController(DictionaryService dictionaryService)
        {
            _dictionaryService = SetOrThrowException(() => dictionaryService);
        }

        /// <summary>
        /// Returns test models
        /// </summary>
        /// <returns></returns>
        [HttpGet("getSomeModels")]
        public async Task<ActionResult<TestModelDto[]>> GetModels()
        {
            var res = await _dictionaryService.GetTestModelsAsync();
            return Ok(res);
        }   
    }
}
