using System.Threading.Tasks;

namespace ProjectWebApi.App.Services
{
    public class TestService : AppServiceBase
    {

        public Task TestAsync()
        {
            return Task.FromResult(true);
        }
    }
}
