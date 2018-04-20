using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GranularMonitorSystem.Services.API.Dashboard
{
    public interface IDashboardService
    {
            Task<AlertContainer> GetAlertsAsync();
    }
}
