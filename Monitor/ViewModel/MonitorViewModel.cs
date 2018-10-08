using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Monitor.Model;
using Monitor.Supervisor;
using Monitor.Services.RequestProvider;
using Monitor.Exceptions;
using Monitor.Localization;

namespace Monitor
{
	public class MonitorViewModel: ViewModelBase 
	{
        private readonly IMonitorService _monitorService;
        private MonitorModel _model;

        public MonitorViewModel( IMonitorService monitorService)
		{
            _monitorService = monitorService;
		}

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
			getStatus((MonitorModel)navigationData);
            IsBusy = false; 
        }

		private void getStatus(MonitorModel monitor)
        {
            _model = monitor;

            URL = monitor.URL;
            StatusCode =  monitor.StatusCode;
            Description = monitor.Description;
            SMS = monitor.SMSAlert; 
            Email =  monitor.EmailAlert;
            Push = monitor.PushAlert;
            Interval = monitor.Interval;
        }

        public ICommand ApplyCommand => new Command(async() => await ApplyCommandAsync());

       
        private async Task ApplyCommandAsync()
        {
            MonitorModel model = new MonitorModel{
                Id = _model.Id,
                UserId = Settings.UserId,
                URL = URL,
                SMSAlert = SMS,
                PushAlert =  Push,
                Interval = Interval
            };

            try
            {
                await _monitorService.EditMonitorAsync(model);
                await DialogService.ShowAlertAsync(AppResources.ChangesApplied,"",AppResources.OK);
                await NavigationService.PopAsync();
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

        private async Task AlertTapCommandAsync(object sender)
        {
            MonitorModel monitor = new MonitorModel()
            {
            };

            MonitorModel editMonitor = await _monitorService.EditMonitorAsync(monitor);
            await DialogService.ShowAlertAsync(AppResources.Successful,AppResources.Save, AppResources.OK);

        }

        string _url;
        public string URL 
        {
            set
            { 
                _url = value;
                RaisePropertyChanged(() => URL );             
            }
            get
            {
                return _url;
            }
        }

		string _statusCode;
		public string StatusCode
		{
			set
            { 
                _statusCode = value;
                RaisePropertyChanged(() => StatusCode);				
			}
			get
			{
				return _statusCode;
			}
		}

        int _interval= 1;
        public int Interval 
        {
            set
            { 
                _interval= value;
                RaisePropertyChanged(() => Interval);             
            }
            get
            {
                return _interval;
            }
        }


		string _description;
		public string Description
		{
			set
			{
					_description= value;
					RaisePropertyChanged(() => Description);
			}
			get
			{
				return _description;
			}
		}

        bool _sms;
        public bool SMS 
        {
            set
            { 
                _sms = value; 
                RaisePropertyChanged(() => SMS);
            }
            get
            {
                return _sms;
            }
        }

        bool _email;
        public bool Email  
        {
            set
            { 
                _email = value; 
                RaisePropertyChanged(() => Email);
            }
            get
            {
                return _email;
            }
        }

        bool _push;
        public bool Push 
        {
            set
            { 
                _push= value; 
                RaisePropertyChanged(() => Push);
            }
            get
            {
                return _push;
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
