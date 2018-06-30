using System;
using System.Threading.Tasks;
using Monitor.Services.Dashboard;
using System.Windows.Input;
using Xamarin.Forms;
using Monitor.Exceptions;
using Monitor.Services.RequestProvider;
using System.Collections.ObjectModel;
using Monitor.Supervisor;

namespace Monitor
{
    public class DashboardViewModel : ViewModelBase

    {
		private readonly  IMonitorService _monitorService;

        public DashboardViewModel(IMonitorService monitorService )
        {
			_monitorService = monitorService;
        }

        public ICommand EditCommand => new Command(async (sender)   => await EditCommandAsync(sender));
        public ICommand AddCommand => new Command(async ()   => await AddCommandAsync());
        public ICommand RefreshMonitorCommand => new Command(async() => await RefreshMonitorCommandAsync());
        public ICommand OnDeleteCommand => new Command(async(sender) => await OnDeleteCommandAsync(sender));

       

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;
                MonitorContainer alertContainer  = await _monitorService.GetMonitorsAsync();
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
              await  NavigationService.NavigateToAsync<MonitorViewModel>(sender);
        }

        private async Task RefreshMonitorCommandAsync()
        {
            IsListViewRefreshing = true;
            MonitorContainer alertContainer  = await _monitorService.GetMonitorsAsync();
            OnUpdate(alertContainer);
            IsListViewRefreshing = false;
        } 

        private async Task OnDeleteCommandAsync(object sender)
        {
            var monitor  = (MonitorModel)sender;
            MonitorContainers.Remove(monitor);
            await _monitorService.DeleteMonitorAsync(monitor);
        }

		public void OnUpdate(MonitorContainer monitorContainer)
        {
            MonitorContainers = new ObservableCollection<MonitorModel>();

			foreach(MonitorModel model in monitorContainer.monitor)
            { 
                MonitorContainers.Add(new MonitorModel{ 
                    Name = model.Name, 
					Description = model.Description,
                    StatusCode = (model.StatusCode == "200") ? "success.png" : "error.png",
                    Id = model.Id,
                    UserId = model.UserId
				});
			}
        }

        bool isListViewRefreshing = false;
        public bool IsListViewRefreshing
        {
            set
            {
                isListViewRefreshing = value;
                RaisePropertyChanged(() => IsListViewRefreshing);
            }
            get
            {
                return isListViewRefreshing;
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

		ObservableCollection<MonitorModel> monitorContainers;
		public  ObservableCollection<MonitorModel> MonitorContainers
        {
            set
            {
                monitorContainers = value;
                RaisePropertyChanged(() => MonitorContainers);
            }
            get
            {
                return monitorContainers;
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
