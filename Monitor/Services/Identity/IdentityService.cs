using System;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Monitor.Model;
using Monitor.Models;
using Monitor.Services.Requests;

namespace Monitor.Services.Identity
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
            UriBuilder builder = new UriBuilder(GlobalRoutingSettings.Instance.TokenEndpoint);
            string uri = builder.ToString();
            User user = await _requests.GetAsync<User>(uri, username, password);
            return user;
        }

		public async Task<User> ResetAsync<User>(User model)
		{
            UriBuilder builder = new UriBuilder(GlobalRoutingSettings.Instance.Reset);
            string uri = builder.ToString();
            User user = await _requests.PostAsync<User>(uri, model);
            return user;
		}

		public async Task<User> SignUpAsync<User>(User model)
        {
            UriBuilder builder = new UriBuilder(GlobalRoutingSettings.Instance.Register);
            string uri = builder.ToString();
            User user = await _requests.PostAsync<User>(uri, model);
            return user;
        }

        public async Task<User> UpgradeAccountAsync<User>(User model)
        {
            UriBuilder builder = new UriBuilder(GlobalRoutingSettings.Instance.UpgradeAccount);
            string uri = builder.ToString();
            User user = await _requests.PostAsync<User>(uri, model);
            return user;
        }

        public async Task<User> UserInfoAsync<User>()
        {
            UriBuilder builder = new UriBuilder($"{GlobalRoutingSettings.Instance.UserInfo}/{Settings.UserId}");
            string uri = builder.ToString();
            User user = await _requests.GetAsync<User>(uri);
            return user;
        }

        public async Task<MobileDevice> DeviceAsync<MobileDevice>(MobileDevice device)
        {
            UriBuilder builder = new UriBuilder($"{GlobalRoutingSettings.Instance.UserInfo}/{Settings.UserId}");
            string uri = builder.ToString();
             await _requests.PostAsync<MobileDevice>(uri, device);
            return device;
        }
    }
}
