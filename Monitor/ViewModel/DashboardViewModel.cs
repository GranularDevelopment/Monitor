using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Monitor.Exceptions;
using Monitor.Services.RequestProvider;
using System.Collections.ObjectModel;
using Monitor.Supervisor;
using Monitor.Localization;
using Monitor.Enums;

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
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            catch (HttpRequestExceptionEx  e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            catch (Exception e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
        }

        private async Task AddCommandAsync()
        {
            if( Settings.AccountType == (int)AccountType.FREE && MonitorContainers?.Count == 0){
                await NavigationService.NavigateToAsync<AddViewModel>();
            }
            else if( Settings.AccountType == (int)AccountType.BASIC && MonitorContainers?.Count < 10){
                await NavigationService.NavigateToAsync<AddViewModel>();
            }
            else if( Settings.AccountType == (int)AccountType.PREMIUM && MonitorContainers?.Count < 50){
                await NavigationService.NavigateToAsync<AddViewModel>();
            }
            else{
                await DialogService.ShowAlertAsync("No monitors available","", AppResources.OK);
            }
        }

        private async Task EditCommandAsync(object sender)
        {
              await  NavigationService.NavigateToAsync<MonitorViewModel>(sender);
        }

        private async Task RefreshMonitorCommandAsync()
        {
            try
            {
                IsListViewRefreshing = true;
                MonitorContainer alertContainer  = await _monitorService.GetMonitorsAsync();
                OnUpdate(alertContainer);
            } 
            catch (ServiceAuthenticationException e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            catch (HttpRequestExceptionEx  e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            catch (Exception e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }finally{
                IsListViewRefreshing = false;

            }
        } 

        private async Task OnDeleteCommandAsync(object sender)
        {
            try
            {
                var monitor  = (MonitorModel)sender;
                MonitorContainers.Remove(monitor);
                await _monitorService.DeleteMonitorAsync(monitor);

            }
            catch (ServiceAuthenticationException e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            catch (HttpRequestExceptionEx  e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            catch (Exception e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }

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
                    UserId = model.UserId,
                    URL = model.URL,
                    Interval = model.Interval,
                    EmailAlert = model.EmailAlert,
                    PushAlert = model.PushAlert,
                    SMSAlert = model.SMSAlert
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

        string name = "";
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

        string statusCode = "";
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

        string description= "";
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

        string url= "";
        public string URL{
            set
            {
                url = value;
                RaisePropertyChanged(() => URL);
            }
            get
            {
                return url;
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
