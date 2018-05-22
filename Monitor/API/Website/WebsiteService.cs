using System;
using System.Threading.Tasks;
using Monitor.Model;
using Monitor.Services.API.Requests;

namespace Monitor.Services.API.Website
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IRequests _request;

        public WebsiteService(IRequests request )
        {
            _request = request; 
        }

        public async Task<WebsiteContainer> GetWebsiteAsync()
        {
            UriBuilder builder = new UriBuilder(Constants.URL_WEBSITE);
            string uri = builder.ToString();
            WebsiteContainer websiteContainer = await _request.GetAsync<WebsiteContainer>(uri);

            return websiteContainer; 
        }

        public async Task<WebsiteContainer> PostWebsiteAlertAsync(WebsiteContainer container)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_WEBSITE_EDIT);
            string uri = builder.ToString();
            WebsiteContainer websiteContainer = await _request.PostAsync(uri, container);

            return websiteContainer;
        }
    }
}
