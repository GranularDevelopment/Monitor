using System;
using System.Threading.Tasks;

namespace GranularMonitorSystem.Services.API.Server
{
    public interface IServerService
    {
        Task<ServerContainer> GetServerAsync();
        Task<ServerCPUContainer> GetServerCPUAsync();
        Task<ServerDiskspaceContainer> GetServerDiskspaceAsync();
        Task<ServerMemoryContainer> GetServerMemoryAsync();
        Task<ServerContainer> PostServerAlertAsync(ServerContainer container);
    }
}
