using System;
using System.Threading.Tasks;
using Monitor.Model;
using Monitor.Services.API.Requests;

namespace Monitor.Services.API.Identity
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
            User user = await _requests.GetAsync<User>(uri, username, password);
            return user;
        }

		public async Task<User> ResetAsync<User>(User model)
		{
			UriBuilder builder = new UriBuilder(Constants.URL_RESET);
            string uri = builder.ToString();
            User user = await _requests.PostAsync<User>(uri, model);
            return user;
		}

		public async Task<User> SignUpAsync<User>(User model)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SIGNUP);
            string uri = builder.ToString();
            User user = await _requests.PostAsync<User>(uri, model);
            return user;
        }
    }
}
