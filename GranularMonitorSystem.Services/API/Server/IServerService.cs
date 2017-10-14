using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Model.Models.Server;

namespace GranularMonitorSystem.Services.API.Server
{
    public interface IServerService
    {
        Task<ServerContainer> GetServerAsync(string token);
        Task<ServerCPUContainer> GetServerCPUAsync(string token);
        Task<ServerDiskspaceContainer> GetServerDiskspaceAsync(string token);
        Task<ServerMemoryContainer> GetServerMemoryAsync(string token);
        Task<ServerContainer> PostServerAlertAsync(ServerContainer container, string token);
    }
}
