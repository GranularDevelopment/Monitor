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
			await getStatus((MonitorModel)navigationData);
            IsBusy = false; 
        }

		private async Task getStatus(MonitorModel navigationData)
        {
			MonitorModel monitor = await _monitorService.GetMonitorAsync(new MonitorModel());

            StatusCode =  monitor.StatusCode;
            Description = monitor.Description;
            On = monitor.On; 
        }

        public ICommand RestartWebsiteCommand => new Command(async(sender) => await restartWebsiteAsync(sender));
        public ICommand GitCommand => new Command(async(sender) => await gitPullAndUpdateAsync(sender));
        public ICommand StopWebsiteCommand => new Command(async(sender) => await stopWebsiteAsync(sender));
        public ICommand AlertsCommand => new Command(async (sender)   => await AlertTapCommandAsync(sender));

       
        private async Task restartWebsiteAsync(object sender)
        {
            await DialogService.ShowAlertAsync("Website Restarted","Restart","OK");
        }

        private Task gitPullAndUpdateAsync(object sender)
        {
            throw new NotImplementedException();
        }

        private Task stopWebsiteAsync(object sender)
        {
            throw new NotImplementedException();
        }


        private async Task AlertTapCommandAsync(object sender)
        {
            MonitorModel monitor = new MonitorModel()
            {
                On = on
            };

           MonitorModel editMonitor = await _monitorService.EditMonitorAsync(monitor);
            await DialogService.ShowAlertAsync("Successful","Save","OK");

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

		bool on;
		public bool On
		{
			set
            { 
                on = value; 
                RaisePropertyChanged(() => On);
			}
			get
			{
				return on;
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
