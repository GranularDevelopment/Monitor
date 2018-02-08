﻿using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Services.API.Dashboard;
using System.Windows.Input;
using Xamarin.Forms;
using GranularMonitorSystem.Exceptions;
using GranularMonitorSystem.Services.RequestProvider;

namespace GranularMonitorSystem
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardViewModel(IDashboardService dashboardService)
        {
            _dashboardService =  dashboardService;
        }

        public ICommand DashboardTapCommand => new Command(async (sender)   => await DashboardTapCommandAsync(sender));

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;
                AlertContainer alertContainer  = await  _dashboardService.GetAlertsAsync();
                OnUpdate(alertContainer);
                IsBusy = false;
            }
            catch (ServiceAuthenticationException e)
            {
                await DialogService.ShowAlertAsync(e.Content, "Authentication Error", "OK");
            }
            catch (HttpRequestExceptionEx  e)
            {
                await DialogService.ShowAlertAsync(e.HttpCode.ToString(), "HTTP Error", "OK");
            }
            catch (Exception e)
            {
                await DialogService.ShowAlertAsync(e.ToString(), "Error", "OK");
            }
        }

        private async Task DashboardTapCommandAsync(object sender)
        {
            if(sender.ToString() == "website")
            {
              await  NavigationService.NavigateToAsync<WebsiteViewModel>();
            }
            else if(sender.ToString() == "server")
            {
                await  NavigationService.NavigateToAsync<ServerViewModel>();
            }
        }

        public void OnUpdate(AlertContainer alertContainer)
        {
            foreach (AlertModel model in alertContainer.alert)
            {
                if (model.Name == "website")
                {
                    WebsiteStatus = (model.StatusCode == "200") ? "Green" : "Red";
                }
                else if (model.Name == "server")
                {
                    ServerStatus = (model.StatusCode == "200") ? "Green" : "Red";
                }
            }
        }

        string websiteStatus = "Gray";
        public string WebsiteStatus
        {
            set
            {
                websiteStatus = value;
                RaisePropertyChanged(() => WebsiteStatus);
                  
            }
            get
            {
                return websiteStatus;
            }
        }

        string serverStatus = "Gray";
        public string ServerStatus
        {
            set
            {
                serverStatus = value;
                RaisePropertyChanged(() => ServerStatus);
            }
            get
            {
                return serverStatus;
            }
        }

        private bool _isBusy;
        public bool IsBusy 
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }
    }
}
