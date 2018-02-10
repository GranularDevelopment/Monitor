using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GranularMonitorSystem.Enums;
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

        public override  async Task InitializeAsync(object navigationData)
        {
            await createPlotView();
        }

        public ICommand TodayCommand => new Command (async() => await TodayCommandAsync() );
        public ICommand WeekCommand => new Command (async() => await WeekCommandAsync() );
        public ICommand MonthCommand => new Command (async() => await MonthCommandAsync() );
       

        private async Task TodayCommandAsync()
        {
            await createPlotView();
        }

        private async Task WeekCommandAsync()
        {
            await createPlotView(TimeRange.WEEK);
        }

        private async Task MonthCommandAsync()
        {
            await createPlotView(TimeRange.MONTH);
        }

        private async Task createPlotView(TimeRange range = TimeRange.DAY)
        {
            try
            {
                await showPlotModel(range);
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

        private async Task showPlotModel(TimeRange range)
        {
            ServerMemoryContainer serverCpuContainer = await getTimeRangeAsync(range); 
            ScatterSeries series = createSeries(serverCpuContainer);
            PlotModel = createPlotModel(series, range); 
        }

        private async Task<ServerMemoryContainer> getTimeRangeAsync(TimeRange range = TimeRange.DAY)
        {
            ServerMemoryContainer  serverMemoryContainer = new ServerMemoryContainer(); 

            if(TimeRange.DAY == range)
            {
                serverMemoryContainer = await _serverService.GetServerMemoryTodayAsync();
            }
            else if (TimeRange.WEEK == range)
            {
                serverMemoryContainer = await _serverService.GetServerMemoryWeekAsync();
            }
            else if(TimeRange.MONTH == range)
            {
                serverMemoryContainer = await _serverService.GetServerMemoryMonthAsync();
            }

            return serverMemoryContainer;
        } 

        private ScatterSeries createSeries(ServerMemoryContainer container)
        {

            ScatterSeries series = new ScatterSeries{
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            foreach(ServerMemoryModel model in container.MemoryModels)
            {
                series.Points.Add(new ScatterPoint((DateTimeAxis.ToDouble(model.Timestamp)), model.Memory));
            }

            return series;
        }

        private PlotModel createPlotModel(ScatterSeries series, TimeRange range)
        {
            DateTimeIntervalType interval = (range == TimeRange.DAY) ? DateTimeIntervalType.Auto : DateTimeIntervalType.Days;

            var plotModel = new PlotModel { Title = "Memory" };

            plotModel.Axes.Add (new DateTimeAxis { Title ="Date", Position = AxisPosition.Bottom, IntervalType = interval});
            plotModel.Axes.Add (new LinearAxis { Position = AxisPosition.Left, Maximum = 100, Minimum = 0 });

            plotModel.Series.Add (series);

            return plotModel;
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
