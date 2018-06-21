using System;
using System.Threading.Tasks;
using Monitor.Model;

namespace Monitor.Services.Identity
{
    public interface IIdentityService
    {
        Task<User> LoginAsync<User>(string username, string password);
        Task<User> SignUpAsync<User>(User model);
		Task<User> ResetAsync<User>(User model);
    }
}
