using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Model;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Services.API.Requests;

namespace GranularMonitorSystem.Services.API.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IRequests _requests; 

        public IdentityService(IRequests requests)
        {
            _requests = requests;
        }

        public async Task<User> LoginAsync<User>(string username, string password )
        {
            UriBuilder builder = new UriBuilder(Constants.URL_TOKEN);
            string uri = builder.ToString();
            User user = await _requests.LoginAsync<User>(uri, username, password);

            return user;
        }
    }
}
