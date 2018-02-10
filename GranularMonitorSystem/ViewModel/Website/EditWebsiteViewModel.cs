using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Model;
using GranularMonitorSystem.Services.API.Website;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
    public class EditWebsiteViewModel: ViewModelBase
    {
        private readonly IWebsiteService _websiteService;

        public EditWebsiteViewModel(IWebsiteService websiteService)
        {
            _websiteService = websiteService;
        }

        public override Task InitializeAsync(object on) 
        {
            On = (bool)on;

            return base.InitializeAsync(on);
        }
        public ICommand AlertsCommand => new Command(async (sender)   => await AlertTapCommandAsync(sender));

        private async Task AlertTapCommandAsync(object sender)
        {
            WebsiteContainer container = new WebsiteContainer()
            {
                On = _on
            };
            WebsiteContainer websiteContainer = await _websiteService.PostWebsiteAlertAsync(container);
            await DialogService.ShowAlertAsync("Successful","Save","OK");

        }


        bool _on;
        public bool On
        {
            set
            {
                _on = value;
                RaisePropertyChanged(() => On);

            }
            get
            {
                return _on;
            }
        }
    }
}
