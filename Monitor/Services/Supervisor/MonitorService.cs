using System;
using System.Threading.Tasks;
using Monitor.Services.Requests;
using Monitor.Supervisor;

namespace Monitor
{
	public class MonitorService: IMonitorService
    {
		private readonly IRequests _request;

        public MonitorService( IRequests request)
        {
			_request = request;
        }

        public async Task<MonitorModel> AddMonitorAsync(MonitorModel monitor)
        {
            UriBuilder builder = new UriBuilder(GlobalSetting.Instance.AddMonitor);
            string uri = builder.ToString();
			MonitorModel monitorContainer = await _request.PostAsync(uri, monitor);

            return monitorContainer; 
        }

		public async Task<MonitorContainer> GetMonitorsAsync()
        {
            UriBuilder builder = new UriBuilder($"{GlobalSetting.Instance.GetMonitors}/{Settings.UserName}");
            string uri = builder.ToString();
            MonitorContainer monitorContainer = await _request.GetAsync<MonitorContainer>(uri);

            return monitorContainer; 
        }

		public async Task<MonitorModel> GetMonitorAsync(MonitorModel model)
        {
            UriBuilder builder = new UriBuilder($"{GlobalSetting.Instance.GetMonitor}/{model.Id}/{model.UserId}");
            string uri = builder.ToString();
            MonitorModel monitorModel= await _request.GetAsync<MonitorModel>(uri);

            return monitorModel; 
        }

		public async Task<MonitorModel> EditMonitorAsync(MonitorModel model)
        {
            UriBuilder builder = new UriBuilder(GlobalSetting.Instance.EditMonitor);
            string uri = builder.ToString();
            MonitorModel monitorModel= await _request.PostAsync<MonitorModel>(uri,model);

            return monitorModel; 
        }

        public async Task<MonitorModel> DeleteMonitorAsync(MonitorModel model)
        {

            UriBuilder builder = new UriBuilder(GlobalSetting.Instance.DeleteMonitor);
            string uri = builder.ToString();
            MonitorModel monitorModel= await _request.PostAsync<MonitorModel>(uri,model);

            return monitorModel; 
        }
    }
}
