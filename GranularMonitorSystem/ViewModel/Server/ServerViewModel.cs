using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GranularMonitorSystem.Common.Utilities;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Windows.Input;
using GranularMonitorSystem.Services.API.Server;
using GranularMonitorSystem.Common;

namespace GranularMonitorSystem
{
    public class ServerViewModel : ViewModelBase
    {
        private readonly IServerService _serverService;

        public ServerViewModel(IServerService serverService) : base()
        {
            _serverService = serverService;
        }

        public override async Task InitializeAsync(object navigationData) 
        {
            IsBusy = true;
            await getStatus();
            IsBusy = false;
        }

        private async Task getStatus()
        {
            ServerContainer serverContainer = await _serverService.GetServerAsync();

            StatusCode = serverContainer.StatusCode;
            Description = serverContainer.Description;
            On = serverContainer.On;
        }

        public ICommand Cpu_Command => new Command(async ()   => await cpuCommandAsync());
        public ICommand Memory_Command => new Command(async ()   => await memoryCommandAsync());
        public ICommand DiskSpace_Command => new Command(async ()   => await diskspaceCommandAsync());
        public ICommand IO_Command => new Command(async ()   => await ioCommandAsync());
        public ICommand AlertsCommand => new Command(async ()   => await alertTapCommandAsync());

        private async Task editSettingsAsync()
        {
            await NavigationService.NavigateToAsync<EditServerViewModel>();
        }

        private async Task cpuCommandAsync()
        {
            await NavigationService.NavigateToAsync<ServerCpuViewModel>();
        }

        private async Task memoryCommandAsync()
        {
            await NavigationService.NavigateToAsync<ServerMemoryViewModel>();
        }

        private async Task diskspaceCommandAsync()
        {
            await NavigationService.NavigateToAsync<ServerDiskSpaceViewModel>();
        }

        private async Task ioCommandAsync()
        {
            await NavigationService.NavigateToAsync<ServerIOViewModel>();
        }

        private async Task alertTapCommandAsync()
        {
            ServerContainer container = new ServerContainer()
            {
                On = _on
            };
            ServerContainer serverContainer = await _serverService.PostServerAlertAsync(container);
            await DialogService.ShowAlertAsync("Successful","Save","OK");
        }

        string _statusCode;
        public string StatusCode
        {
            set
            {
                _statusCode = value;
                RaisePropertyChanged(()=> StatusCode);
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
                _description = value;
                RaisePropertyChanged(()=> Description);

            }
            get
            {
                return _description;
            }
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

        bool _isBusy;
        public bool IsBusy
        {
            set
            {
                    _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
            get
            {
                return _isBusy;
            }
        }
    }
}
