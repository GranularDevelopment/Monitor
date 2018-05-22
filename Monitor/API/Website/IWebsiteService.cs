using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monitor.Model;

namespace Monitor.Services.API.Website
{
    public interface IWebsiteService
    {
        Task<WebsiteContainer> GetWebsiteAsync();
        Task<WebsiteContainer> PostWebsiteAlertAsync( WebsiteContainer container);
    }
}
