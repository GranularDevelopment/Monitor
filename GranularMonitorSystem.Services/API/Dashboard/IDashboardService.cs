using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Model.Models.Alert;

namespace GranularMonitorSystem.Services.API.Dashboard
{
    public interface IDashboardService
    {
            Task<AlertContainer> GetAlertsAsync(string token);
    }
}
