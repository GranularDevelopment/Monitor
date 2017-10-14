using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Model;

namespace GranularMonitorSystem.Services.API.Identity
{
    public interface IIdentityService
    {
        Task<User> LoginAsync<User>(string username, string password);
    }
}
