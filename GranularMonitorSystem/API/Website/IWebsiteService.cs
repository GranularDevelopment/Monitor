using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GranularMonitorSystem.Model;

namespace GranularMonitorSystem.Services.API.Website
{
    public interface IWebsiteService
    {
        Task<WebsiteContainer> GetWebsiteAsync();
        Task<WebsiteContainer> PostWebsiteAlertAsync( WebsiteContainer container);
    }
}
