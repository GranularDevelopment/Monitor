using System;
using System.Threading.Tasks;

namespace GranularMonitorSystem.Services.API.Dashboard
{
    public interface IDashboardService
    {
            Task<AlertContainer> GetAlertsAsync();
    }
}
