using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Monitor.Exceptions;
using Monitor.Services.RequestProvider;
using Monitor.Validation;
using Xamarin.Forms;

namespace Monitor
{
    public class AddViewModel:ViewModelBase
    {
		private ValidatableObject<string> _name;
		private ValidatableObject<string> _url;
        private int _interval;

		private readonly  IMonitorService _monitorService;

        private void SliderChanged(object obj)
        {
             _interval = Convert.ToInt16(Math.Round(Convert.ToDouble(obj))); 
        }

		public ICommand ValueChangedCommand => new Command((obj) => SliderChanged(obj));
		public ICommand AddMonitorCommand => new Command(async () => await AddMonitorAsync());

		private async Task AddMonitorAsync()
		{
            MonitorModel addMonitor = new MonitorModel{
                Interval = Interval,
                Name = Name.Value.Trim(),
                URL = URL.Value.Trim(),
                UserId = Settings.UserId 
			};

			try
			{
			    await _monitorService.AddMonitorAsync(addMonitor);
				await DialogService.ShowAlertAsync("You have successfully added a monitor. ","Added"
                                                     ,"OK");
                await NavigationService.PopAsync();

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

		public AddViewModel(IMonitorService monitor)
        {
			_monitorService = monitor;
			_name = new ValidatableObject<string>();
			_url= new ValidatableObject<string>();
        }

		public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ValidatableObject<string> Name 
        {
            set
            { 
                _name= value;
                RaisePropertyChanged(() => Name);             
            }
            get
            {
                return _name;
            }
        }

        public int Interval 
        {
            set
            { 
                _interval= value;
                RaisePropertyChanged(() => Interval);             
            }
            get
            {
                return _interval;
            }
        }
        
        public ValidatableObject<string> URL 
        {
            set
            { 
                _url = value;
                RaisePropertyChanged(() => URL);             
            }
            get
            {
                return _url;
            }
        }

        private bool ValidateName()
        {
            return _name.Validate();
        }

        private bool ValidateURL()
        {
            return _url.Validate();
        }

		private void addValidations()
        {
            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });
            _url.Validations.Add(new URLRule<string> { ValidationMessage = "A URL is required." });
        }
    }
}
