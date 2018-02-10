using System;
using System.Threading.Tasks;
using GranularMonitorSystem;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Services.API.Requests;

namespace GranularMonitorSystem.Services.API.Server
{
    public class ServerService: IServerService
    {
        private readonly IRequests _request;

        public ServerService(IRequests request)
        {
            _request = request;
        }

        public async Task<ServerContainer> GetServerAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER);
            string uri = builder.ToString();
            ServerContainer serverContainer = await _request.GetAsync<ServerContainer>(uri);

            return serverContainer; 
        }

        public async Task<ServerCPUContainer> GetServerCPUTodayAsync()
        {    
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_CPU_TODAY);
            string uri = builder.ToString();
            ServerCPUContainer serverContainer = await _request.GetAsync<ServerCPUContainer>(uri);

            return serverContainer; 
        }

        public async Task<ServerCPUContainer> GetServerCPUWeekAsync()
        {    
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_CPU_WEEK);
            string uri = builder.ToString();
            ServerCPUContainer serverContainer = await _request.GetAsync<ServerCPUContainer>(uri);

            return serverContainer; 
        }

        public async Task<ServerCPUContainer> GetServerCPUMonthAsync()
        {    
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_CPU_MONTH);
            string uri = builder.ToString();
            ServerCPUContainer serverContainer = await _request.GetAsync<ServerCPUContainer>(uri);

            return serverContainer; 
        }

        public async Task<ServerDiskspaceContainer> GetServerDiskspaceTodayAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_DISKSPACE_TODAY);
            string uri = builder.ToString();
            ServerDiskspaceContainer serverDiskspaceContainer = await _request.GetAsync<ServerDiskspaceContainer>(uri);

            return serverDiskspaceContainer;
        }

        public async Task<ServerDiskspaceContainer> GetServerDiskspaceWeekAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_DISKSPACE_WEEK);
            string uri = builder.ToString();
            ServerDiskspaceContainer serverDiskspaceContainer = await _request.GetAsync<ServerDiskspaceContainer>(uri);

            return serverDiskspaceContainer;
        }

        public async Task<ServerDiskspaceContainer> GetServerDiskspaceMonthAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_DISKSPACE_MONTH);
            string uri = builder.ToString();
            ServerDiskspaceContainer serverDiskspaceContainer = await _request.GetAsync<ServerDiskspaceContainer>(uri);

            return serverDiskspaceContainer;
        }

        public async Task<ServerMemoryContainer> GetServerMemoryTodayAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_MEMORY_TODAY);
            string uri = builder.ToString();
            ServerMemoryContainer serverMemoryContainer = await _request.GetAsync<ServerMemoryContainer>(uri);

            return serverMemoryContainer;
        }

        public async Task<ServerMemoryContainer> GetServerMemoryWeekAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_MEMORY_WEEK);
            string uri = builder.ToString();
            ServerMemoryContainer serverMemoryContainer = await _request.GetAsync<ServerMemoryContainer>(uri);

            return serverMemoryContainer;
        }

        public async Task<ServerMemoryContainer> GetServerMemoryMonthAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_MEMORY_MONTH);
            string uri = builder.ToString();
            ServerMemoryContainer serverMemoryContainer = await _request.GetAsync<ServerMemoryContainer>(uri);

            return serverMemoryContainer;
        }

        public async Task<ServerContainer> PostServerAlertAsync(ServerContainer container)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_EDIT);
            string uri = builder.ToString();
            ServerContainer serverContainer = await _request.PostAsync(uri, container);

            return serverContainer; 
        }
    }
}
