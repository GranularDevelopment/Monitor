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
using GranularMonitorSystem.Model.Models.Server;

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
            var token = Constants.TOKEN;
            ServerContainer serverContainer = await _serverService.GetServerAsync(token);

            StatusCode = serverContainer.StatusCode;
            Description = serverContainer.Description;
            On = serverContainer.On;
        }

        public ICommand Cpu_Command => new Command(async ()   => await cpuCommandAsync());
        public ICommand Memory_Command => new Command(async ()   => await memoryCommandAsync());
        public ICommand DiskSpace_Command => new Command(async ()   => await diskspaceCommandAsync());
        public ICommand IO_Command => new Command(async ()   => await ioCommandAsync());
        public ICommand AlertsCommand => new Command(async (sender)   => await AlertTapCommandAsync(sender));

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

        private async Task AlertTapCommandAsync(object sender)
        {
            ServerContainer container = new ServerContainer()
            {
                On = _on
            };
            var token = Constants.TOKEN;
            ServerContainer serverContainer = await _serverService.PostServerAlertAsync(container, token);
            await DialogService.ShowAlertAsync("Successful","Save","OK");
        }
        //public override async Task OnLoad()
        //{
        //    //create a flag or update on pull down. updates every time...
        //    //try
        //    //{
        //    //    IsBusy = true;
        //    //    serverContainer = await serverServiceGet.GetServerData();
        //    //    serverModels = serverContainer.server;
        //    //    await OnUpdate();
        //    //    IsBusy = false;
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    await errorHandler.DisplayPopup("Exception", e.ToString(), "OK");
        //    //}
        //}

        //public override async Task OnSave()
        //{
        //    //try
        //    //{
        //    //    serverContainer.On = On;
        //    //    await serverServicePost.PostServerData(serverContainer);
        //    //    await errorHandler.DisplayPopup("Saved", "Updates have been saved", "OK");
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    await errorHandler.DisplayPopup("Exception", e.Message, "OK");
        //    //}
        //}

        //public override void OnEdit()
        //{
        //    throw new NotImplementedException();
        //}

        //public override async Task OnUpdate()
        //{
        //    //try
        //    //{
        //    //    convertDataForOxyPlot();
                
        //    //    StatusCode = serverContainer.StatusCode;
        //    //    Description = serverContainer.Description;
        //    //    On = serverContainer.On;
        //    //    DateRange = dateRange;
        //    //    Cpu = cpu;
        //    //    Memory = memory;
        //    //    IO = io;
        //    //    Diskused = diskused;
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    await errorHandler.DisplayPopup("Exception", e.ToString(), "OK");
        //    //}
        //}

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
