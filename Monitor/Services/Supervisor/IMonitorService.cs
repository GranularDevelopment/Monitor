using System;
using System.Threading.Tasks;
using Monitor.Supervisor;

namespace Monitor
{
    public interface IMonitorService
    {
		Task<MonitorModel> AddMonitorAsync(MonitorModel monitor);
		Task<MonitorModel> GetMonitorAsync(MonitorModel monitor);
		Task<MonitorContainer> GetMonitorsAsync();
		Task<MonitorModel> EditMonitorAsync(MonitorModel model);
    }
}
