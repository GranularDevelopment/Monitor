using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Services.API.Dashboard;
using System.Windows.Input;
using Xamarin.Forms;
using GranularMonitorSystem.Exceptions;
using GranularMonitorSystem.Services.RequestProvider;
using System.Collections.ObjectModel;

namespace GranularMonitorSystem
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardViewModel(IDashboardService dashboardService)
        {
            _dashboardService =  dashboardService;
        }

        public ICommand EditCommand => new Command(async (sender)   => await EditCommandAsync(sender));
        public ICommand AddCommand => new Command(async ()   => await AddCommandAsync());

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

        private async Task AddCommandAsync()
        {
            await NavigationService.NavigateToAsync<AddViewModel>();
        }

        private async Task EditCommandAsync(object sender)
        {
            AlertModel monitor = (AlertModel)sender; 
            if(monitor.Name.ToString() == "website")
            {
              await  NavigationService.NavigateToAsync<WebsiteViewModel>();
            }
            else if(monitor.Name.ToString() == "server")
            {
                await  NavigationService.NavigateToAsync<ServerViewModel>();
            }
        }

        public void OnUpdate(AlertContainer alertContainer)
        {
            AlertContainers = new ObservableCollection<AlertModel>();

            foreach(AlertModel model in alertContainer.alert)
            { 
                if (model.Name == "website")
                {
                    WebsiteStatus = (model.StatusCode == "200") ? "status-ok" : "status-error";
                }
                else if (model.Name == "server")
                {
                    ServerStatus = (model.StatusCode == "200") ? "status-ok" : "status-error";
                }

                AlertContainers.Add(new AlertModel{
                    StatusCode = model.StatusCode,
                    Name = model.Name,
                    Description = model.Description
                });
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

        string name = "Gray";
        public string Name 
        {
            set
            {
                name = value;
                RaisePropertyChanged(() => Name);

            }
            get
            {
                return name;
            }
        }

        string statusCode = "Gray";
        public string StatusCode 
        {
            set
            {
                statusCode= value;
                RaisePropertyChanged(() => StatusCode);
            }
            get
            {
                return statusCode;
            }
        }

        string description= "Gray";
        public string Description {
            set
            {
                description = value;
                RaisePropertyChanged(() => Description);
            }
            get
            {
                return description;
            }
        }

        ObservableCollection<AlertModel> alertContainers;
        public  ObservableCollection<AlertModel> AlertContainers
        {
            set
            {
                alertContainers = value;
                RaisePropertyChanged(() => AlertContainers);
            }
            get
            {
                return alertContainers;
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
