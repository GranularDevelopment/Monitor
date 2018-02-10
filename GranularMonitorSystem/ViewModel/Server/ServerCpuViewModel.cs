using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Common;
using GranularMonitorSystem.Services.API.Server;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Xamarin.Forms;
using OxyPlot.Axes;
using OxyPlot.Series;
using GranularMonitorSystem.Exceptions;
using GranularMonitorSystem.Services.RequestProvider;
using System.Collections.ObjectModel;
using GranularMonitorSystem.Enums;
using System.Windows.Input;

namespace GranularMonitorSystem
{
    public class ServerCpuViewModel: ViewModelBase
    {
        private readonly IServerService _serverService;

        public ServerCpuViewModel(IServerService serverService)
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
            ServerCPUContainer  serverCpuContainer = await getTimeRangeAsync(range); 
            ScatterSeries series = createSeries(serverCpuContainer);
            PlotModel = createPlotModel(series, range); 
        }

        private async Task<ServerCPUContainer> getTimeRangeAsync(TimeRange range = TimeRange.DAY)
        {
            ServerCPUContainer  serverCpuContainer = new ServerCPUContainer(); 

            if(TimeRange.DAY == range)
            {
                serverCpuContainer = await _serverService.GetServerCPUTodayAsync();
            }
            else if (TimeRange.WEEK == range)
            {
                serverCpuContainer = await _serverService.GetServerCPUWeekAsync();
            }
            else if(TimeRange.MONTH == range)
            {
                serverCpuContainer = await _serverService.GetServerCPUMonthAsync();
            }

            return serverCpuContainer;
        } 

        private ScatterSeries createSeries(ServerCPUContainer container)
        {
            
            ScatterSeries series = new ScatterSeries{
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            foreach(ServerCPUModel model in container.CpuModels)
            {
                series.Points.Add(new ScatterPoint((DateTimeAxis.ToDouble(model.Timestamp)), model.CPU));
            }

            return series;
        }

        private PlotModel createPlotModel(ScatterSeries series, TimeRange range)
        {
            DateTimeIntervalType interval = (range == TimeRange.DAY) ? DateTimeIntervalType.Auto : DateTimeIntervalType.Days;

            var plotModel = new PlotModel { Title = "CPU" };

            plotModel.Axes.Add (new DateTimeAxis { Title ="Date", Position = AxisPosition.Bottom, IntervalType = interval});
            plotModel.Axes.Add (new LinearAxis { Position = AxisPosition.Left, Maximum = 100, Minimum = 0 });

            plotModel.Series.Add (series);

            return plotModel;
        }


        ObservableCollection<ServerCPUContainer>  _serverCpuContainer ;
        public ObservableCollection<ServerCPUContainer> CPUContainer
        {
            get { return _serverCpuContainer;}
            set 
            {
                _serverCpuContainer = value;
                RaisePropertyChanged(() => CPUContainer);
            }
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
