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

        public async Task<ServerContainer> GetServerAsync(string token)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER);
            string uri = builder.ToString();
            ServerContainer serverContainer = await _request.GetAsync<ServerContainer>(uri, token);

            return serverContainer; 
        }

        public async Task<ServerCPUContainer> GetServerCPUAsync( string token)
        {    
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_CPU);
            string uri = builder.ToString();
            ServerCPUContainer serverContainer = await _request.GetAsync<ServerCPUContainer>(uri, token);

            return serverContainer; 
        }

        public async Task<ServerDiskspaceContainer> GetServerDiskspaceAsync(string token)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_DISKSPACE);
            string uri = builder.ToString();
            ServerDiskspaceContainer serverDiskspaceContainer = await _request.GetAsync<ServerDiskspaceContainer>(uri, token);

            return serverDiskspaceContainer;
        }

        public async Task<ServerMemoryContainer> GetServerMemoryAsync(string token)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_MEMORY);
            string uri = builder.ToString();
            ServerMemoryContainer serverMemoryContainer = await _request.GetAsync<ServerMemoryContainer>(uri, token);

            return serverMemoryContainer;
        }

        public async Task<ServerContainer> PostServerAlertAsync(ServerContainer container, string token)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_SERVER_EDIT);
            string uri = builder.ToString();
            ServerContainer serverContainer = await _request.PostAsync(uri, container, token);

            return serverContainer; 
        }
    }
}
