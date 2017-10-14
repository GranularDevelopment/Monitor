﻿using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Model;
using GranularMonitorSystem.Model.Models.Alert;
using GranularMonitorSystem.Services.API.Requests;

namespace GranularMonitorSystem.Services.API.Dashboard
{
    public class DashboardService: IDashboardService
    {
        private readonly IRequests _request;

        public DashboardService(IRequests request)
        {
            _request = request;
        }

        public async Task<AlertContainer> GetAlertsAsync(string token)
        {
            UriBuilder builder = new UriBuilder(Constants.URL_ALERT);
            string uri = builder.ToString();
            AlertContainer alertContainer = await _request.GetAsync<AlertContainer>(uri, token);

            return alertContainer;
        }
    }
}
