using System;
using System.Threading.Tasks;

namespace GranularMonitorSystem.Services.API.Server
{
    public interface IServerService
    {
        Task<ServerContainer> GetServerAsync();

        Task<ServerCPUContainer> GetServerCPUTodayAsync();
        Task<ServerCPUContainer> GetServerCPUWeekAsync();
        Task<ServerCPUContainer> GetServerCPUMonthAsync();

        Task<ServerMemoryContainer> GetServerMemoryTodayAsync();
        Task<ServerMemoryContainer> GetServerMemoryWeekAsync();
        Task<ServerMemoryContainer> GetServerMemoryMonthAsync();

        Task<ServerDiskspaceContainer> GetServerDiskspaceTodayAsync();
        Task<ServerDiskspaceContainer> GetServerDiskspaceWeekAsync();
        Task<ServerDiskspaceContainer> GetServerDiskspaceMonthAsync();

        Task<ServerContainer> PostServerAlertAsync(ServerContainer container);
    }
}
