using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Model.Models.Server;
using GranularMonitorSystem.Services.API.Server;
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

        public override async Task InitializeAsync(object navigationData)
        {
        }

        public async Task<PlotModel> CreatePlotModel() {

            ServerDiskspaceContainer serverDiskspaceContainer = await _serverService.GetServerDiskspaceAsync(Constants.TOKEN);

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

            return plotModel;
        }
    }
}
