using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GranularMonitorSystem.Exceptions;
using GranularMonitorSystem.Services.API.Server;
using GranularMonitorSystem.Services.RequestProvider;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
    public class ServerMemoryViewModel: ViewModelBase
    {
        private readonly IServerService _serverService;

        public ServerMemoryViewModel(IServerService serverService)
        {
            _serverService = serverService;
        }

        public ICommand TodayCommand => new Command (async() => await TodayCommandAsyn() );

        private async Task TodayCommandAsyn()
        {
            await DialogService.ShowAlertAsync("M","T", "ok");
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

        public async Task CreatePlotModel() 
        {
            ServerMemoryContainer serverMemoryContainer = await _serverService.GetServerMemoryAsync();

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

            PlotModel = plotModel;
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
