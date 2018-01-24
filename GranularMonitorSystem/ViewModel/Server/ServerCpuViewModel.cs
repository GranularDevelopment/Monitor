using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Services.API.Server;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Xamarin.Forms;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace GranularMonitorSystem
{
    public class ServerCpuViewModel: ViewModelBase
    {
        private readonly IServerService _serverService;

        public ServerCpuViewModel(IServerService serverService)
        { 
            _serverService = serverService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public async Task<PlotModel> CreatePlotModel() {
           
            ServerCPUContainer  serverCpuContainer = await _serverService.GetServerCPUAsync(Constants.TOKEN);

           var series1 = new LineSeries {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            foreach(ServerCPUModel model in serverCpuContainer.CpuModels)
            {
                series1.Points.Add(new DataPoint((DateTimeAxis.ToDouble(model.Timestamp)), model.CPU));
            }

            var plotModel = new PlotModel { Title = "CPU" };

            plotModel.Axes.Add (new DateTimeAxis { Title ="Date", Position = AxisPosition.Bottom, IntervalType = DateTimeIntervalType.Days});
            plotModel.Axes.Add (new LinearAxis { Position = AxisPosition.Left, Maximum = 100, Minimum = 0 });

            plotModel.Series.Add (series1);

            return plotModel;
    }
  }
}
