using System;
using System.Threading.Tasks;
using Monitor.Services.API.Requests;

namespace Monitor.Services.API.Dashboard
{
    public class DashboardService: IDashboardService
    {
        private readonly IRequests _request;

        public DashboardService(IRequests request)
        {
            _request = request;
        }

        public async Task<AlertContainer> GetAlertsAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_ALERT);
            string uri = builder.ToString();
            AlertContainer alertContainer = await _request.GetAsync<AlertContainer>(uri);

            return alertContainer;
        }
    }
}
