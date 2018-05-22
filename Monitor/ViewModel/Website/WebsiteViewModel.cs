using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Monitor.Services.API.Website;
using Monitor.Model;

namespace Monitor
{
	public class WebsiteViewModel: ViewModelBase 
	{
        private readonly IWebsiteService _websiteService;

        public WebsiteViewModel( IWebsiteService websiteService)
		{
            _websiteService = websiteService;

		}

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            await getStatus();
            IsBusy = false; 
        }

        private async Task getStatus()
        {
            WebsiteContainer websiteContainer = await _websiteService.GetWebsiteAsync();

            StatusCode =  websiteContainer.StatusCode;
            Description = websiteContainer.Description;
            On = websiteContainer.On; 
        }

        public ICommand RestartWebsiteCommand => new Command(async(sender) => await restartWebsiteAsync(sender));
        public ICommand GitCommand => new Command(async(sender) => await gitPullAndUpdateAsync(sender));
        public ICommand StopWebsiteCommand => new Command(async(sender) => await stopWebsiteAsync(sender));
        public ICommand EditWebsiteCommand => new Command(async(sender) => await editWebsiteAsync(sender));
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

        private async Task editWebsiteAsync(object sender)
        {
            await NavigationService.NavigateToAsync<EditWebsiteViewModel>(on); 
        }

        private async Task AlertTapCommandAsync(object sender)
        {
            WebsiteContainer container = new WebsiteContainer()
            {
                On = on
            };
            WebsiteContainer websiteContainer = await _websiteService.PostWebsiteAlertAsync(container);
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
