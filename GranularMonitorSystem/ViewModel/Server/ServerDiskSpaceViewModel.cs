using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GranularMonitorSystem.Common;
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
    public class ServerDiskSpaceViewModel: ViewModelBase
    {
        private readonly IServerService _serverService;

        public ServerDiskSpaceViewModel(IServerService serverService)
        {
            _serverService = serverService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await createPlotView();
        }

        public ICommand TodayCommand => new Command(async () => await TodayCommandAsync());
        public ICommand WeekCommand => new Command(async () => await WeekCommandAsync());
        public ICommand MonthCommand => new Command(async () => await MonthCommandAsync());

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
            ServerDiskspaceContainer  serverDiskspaceContainer = await getTimeRangeAsync(range); 
            ScatterSeries series = createSeries(serverDiskspaceContainer);
            PlotModel = createPlotModel(series, range); 
        }

        private async Task<ServerDiskspaceContainer> getTimeRangeAsync(TimeRange range = TimeRange.DAY)
        {
            ServerDiskspaceContainer  serverDiskspaceContainer = new ServerDiskspaceContainer(); 

            if(TimeRange.DAY == range)
            {
                serverDiskspaceContainer = await _serverService.GetServerDiskspaceTodayAsync();
            }
            else if (TimeRange.WEEK == range)
            {
                serverDiskspaceContainer = await _serverService.GetServerDiskspaceWeekAsync();
            }
            else if(TimeRange.MONTH == range)
            {
                serverDiskspaceContainer = await _serverService.GetServerDiskspaceMonthAsync();
            }

            return serverDiskspaceContainer;
        } 

        private ScatterSeries createSeries(ServerDiskspaceContainer container)
        {

            ScatterSeries series = new ScatterSeries{
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            foreach(ServerDiskspaceModel model in container.DiskspaceModel)
            {
                series.Points.Add(new ScatterPoint((DateTimeAxis.ToDouble(model.Timestamp)), model.Diskused));
            }

            return series;
        }

        private PlotModel createPlotModel(ScatterSeries series, TimeRange range)
        {
            DateTimeIntervalType interval = (range == TimeRange.DAY) ? DateTimeIntervalType.Auto : DateTimeIntervalType.Days;

            var plotModel = new PlotModel { Title = "Disk Space" };

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
