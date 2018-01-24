using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Services.API.Server;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace GranularMonitorSystem
{
    public class ServerMemoryViewModel: ViewModelBase
    {
        private readonly IServerService _serverService;

        public ServerMemoryViewModel(IServerService serverService)
        {
            _serverService = serverService;
        }

        public override  Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public async Task<PlotModel> CreatePlotModel() 
        {
            ServerMemoryContainer serverMemoryContainer = await _serverService.GetServerMemoryAsync(Constants.TOKEN);

            var series1 = new LineSeries {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            foreach(ServerMemoryModel model in serverMemoryContainer.MemoryModels)
            {
                series1.Points.Add(new DataPoint((DateTimeAxis.ToDouble(model.Timestamp)), model.Memory));
            }

            var plotModel = new PlotModel { Title = "Memory" };

            plotModel.Axes.Add (new DateTimeAxis { Title ="Date", Position = AxisPosition.Bottom, IntervalType = DateTimeIntervalType.Days});
            plotModel.Axes.Add (new LinearAxis { Position = AxisPosition.Left, Maximum = 100, Minimum = 0 });

            plotModel.Series.Add (series1);

            return plotModel;
        }
    }
}
