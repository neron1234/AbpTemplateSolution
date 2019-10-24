using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectWebApi.App.Services;
using System.Threading.Tasks;

namespace ProjectWebApi.Controllers
{
    /// <summary>
    /// For testing
    /// </summary>
    [Route("api/[controller]")]
    public class A_TestController : ProjectController
    {
        private readonly TestService _testService;

        /// <summary>
        /// for testing
        /// </summary>
        /// <param name="testService"></param>
        public A_TestController(TestService testService)
        {
            _testService = SetOrThrowException(() => testService);
        }

        /// <summary>
        /// Go
        /// </summary>
        [HttpPost("Test")]
        public async Task<ObjectResult> Test([FromBody] MyClass model)
        {
            await _testService.TestAsync();
            return Ok(model);
        }

        /// <summary>
        /// Check Jwt auth
        /// </summary>
        [HttpPost("TestJwt")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ObjectResult TestJwt()
        {
            return Ok(User.Identity.Name);
        }
    }

    public class MyClass
    {
        public string Name { get; set; }
    }
}
