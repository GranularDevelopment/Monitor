using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Model;
using GranularMonitorSystem.Services.API.Requests;

namespace GranularMonitorSystem.Services.API.Website
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IRequests _request;

        public WebsiteService(IRequests request )
        {
            _request = request; 
        }

        public async Task<WebsiteContainer> GetWebsiteAsync(string token)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_WEBSITE);
            string uri = builder.ToString();
            WebsiteContainer websiteContainer = await _request.GetAsync<WebsiteContainer>(uri, token : token);

            return websiteContainer; 
        }

        public async Task<WebsiteContainer> PostWebsiteAlertAsync(WebsiteContainer container, string token)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_WEBSITE_EDIT);
            string uri = builder.ToString();
            WebsiteContainer websiteContainer = await _request.PostAsync(uri, container, token);

            return websiteContainer;
        }
    }
}
