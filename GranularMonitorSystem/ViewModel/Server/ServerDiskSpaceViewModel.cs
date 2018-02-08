﻿using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Exceptions;
using GranularMonitorSystem.Services.API.Server;
using GranularMonitorSystem.Services.RequestProvider;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace GranularMonitorSystem
{
    public class ServerDiskSpaceViewModel: ViewModelBase
    {
        private readonly IServerService _serverService;

        public ServerDiskSpaceViewModel(IServerService serverService)
        {
            _serverService = serverService;
        }

        public override  async Task InitializeAsync(object navigationData)
        {
            try
            {
                await CreatePlotModel();
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

        public async Task CreatePlotModel() {

            ServerDiskspaceContainer serverDiskspaceContainer = await _serverService.GetServerDiskspaceAsync();

            var series1 = new LineSeries {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            foreach(ServerDiskspaceModel model in serverDiskspaceContainer.DiskspaceModel)
            {
                series1.Points.Add(new DataPoint((DateTimeAxis.ToDouble(model.Timestamp)), model.Diskused));
            }

            var plotModel = new PlotModel { Title = "Diskspace" };

            plotModel.Axes.Add (new DateTimeAxis { Title ="Date", Position = AxisPosition.Bottom, IntervalType = DateTimeIntervalType.Days});
            plotModel.Axes.Add (new LinearAxis { Position = AxisPosition.Left, Maximum = 100, Minimum = 0 });

            plotModel.Series.Add (series1);

            PlotModel=plotModel;
        }

        private PlotModel _plotModel;
        public PlotModel PlotModel
        {
            get {return _plotModel;}
            set 
            {
                _plotModel=value; 
                RaisePropertyChanged(() => PlotModel);
            }
        }
    }
}
