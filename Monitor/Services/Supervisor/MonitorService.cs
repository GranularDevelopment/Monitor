using System;
using System.Threading.Tasks;
using Monitor.Services.Requests;
using Monitor.Supervisor;

namespace Monitor
{
	public class MonitorService: IMonitorService
    {
		private readonly IRequests _request;
		private const string ApiUrlBase = "/api/v1/c/catalog";

        public MonitorService( IRequests request)
        {
			_request = request;
        }

        public async Task<MonitorModel> AddMonitorAsync(MonitorModel monitor)
        {
			UriBuilder builder = new UriBuilder(Constants.URL_ADD_MONITOR);
            string uri = builder.ToString();
			 MonitorModel websiteContainer = await _request.PostAsync(uri, monitor);

            return websiteContainer; 
        }

		public async Task<MonitorContainer> GetMonitorsAsync()
        {
			UriBuilder builder = new UriBuilder(Constants.URL_GET_MONITORS);
            string uri = builder.ToString();
            MonitorContainer monitorContainer = await _request.GetAsync<MonitorContainer>(uri);

            return monitorContainer; 
        }

		public async Task<MonitorModel> GetMonitorAsync(MonitorModel model)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_GET_MONITORS);
            string uri = builder.ToString();
            MonitorModel monitorModel= await _request.GetAsync<MonitorModel>(uri);

            return monitorModel; 
        }

		public async Task<MonitorModel> EditMonitorAsync(MonitorModel model)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_GET_MONITORS);
			//UriBuilder builder = new UriBuilder(GlobalSetting.Instance.BaseEndpoint);
            builder.Path = $"{ApiUrlBase}/catalogtypes";
            string uri = builder.ToString();
            MonitorModel monitorModel= await _request.PostAsync<MonitorModel>(uri,model);

            return monitorModel; 
        }
    }
}
