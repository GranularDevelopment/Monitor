using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Monitor.Model;
using Monitor.Supervisor;

namespace Monitor
{
	public class MonitorViewModel: ViewModelBase 
	{
        private readonly IMonitorService _monitorService;

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
            try
            {
                URL = monitor.URL;
                StatusCode =  monitor.StatusCode;
                Description = monitor.Description;
                SMS = monitor.SMSAlert; 
                Email =  monitor.EmailAlert;
                Push = monitor.PushAlert;
            }
            catch(Exception e)
            {
                
            }
        }

        public ICommand ApplyCommand => new Command(async() => await ApplyCommandAsync());

       
        private async Task ApplyCommandAsync()
        {
            await DialogService.ShowAlertAsync("Your changes have been applied","","OK");
        }

        private async Task AlertTapCommandAsync(object sender)
        {
            MonitorModel monitor = new MonitorModel()
            {
            };

           MonitorModel editMonitor = await _monitorService.EditMonitorAsync(monitor);
            await DialogService.ShowAlertAsync("Successful","Save","OK");

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

		//bool _alerts;
		//public bool Alerts 
		//{
		//	set
  //          { 
  //              _alerts = value; 
  //              RaisePropertyChanged(() => Alerts);
		//	}
		//	get
		//	{
		//		return _alerts;
		//	}
		//}

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
