using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Monitor.Services.Dashboard
{
    public interface IDashboardService
    {
            Task<AlertContainer> GetAlertsAsync();
    }
}
