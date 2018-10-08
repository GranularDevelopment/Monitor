using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Monitor.Exceptions;
using Monitor.Localization;
using Monitor.Model;
using Monitor.Services.Identity;
using Monitor.Services.RequestProvider;
using Monitor.Validation;
using Xamarin.Forms;

namespace Monitor
{
    public class ForgotPasswordViewModel: ViewModelBase  
    {

		private ValidatableObject<string> _userName;

        private readonly IIdentityService _userService;

        public ForgotPasswordViewModel(IIdentityService userService)
        {
            _userService = userService;
            _userName = new ValidatableObject<string>();

            AddValidations();
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand ResetCommand => new Command(async () => await ResetAsync());

        private async Task ResetAsync()
        {
            IsBusy = true;
            await resetRequestAsync();
            IsBusy = false;
        }

        async Task resetRequestAsync()
        {
            if(!Validate())
                return;

            User user = new User{
                UserName = _userName.Value,
            };
            try
            {
                await _userService.ResetAsync<User>(user);
                await DialogService.ShowAlertAsync(AppResources.ForgotPassword, 
                                                   user.UserName,
                                                   AppResources.OK);
                Username = new ValidatableObject<string>();
                await NavigationService.PopAsync();
            }
            catch (ServiceAuthenticationException e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            catch (HttpRequestExceptionEx  e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            catch (Exception e)
            {
                await DialogService.ShowAlertAsync(AppResources.GenericError, AppResources.Error, AppResources.OK);
            }
            
        }

        public ValidatableObject<string> Username 
        { 
            set 
            {
                _userName = value;
                RaisePropertyChanged(() => Username);
            }
            get
            {
                return _userName;
            }
        }

        bool isValid;
        public bool IsValid
        {
            private set
            {
                if (isValid != value)
                {
                    isValid = value;
                }
            }
            get { 
                return isValid; 
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }
        private bool Validate()
        {
            bool isValidUser = ValidateUserName();

            return isValidUser;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new EmailRule<string> { ValidationMessage = AppResources.EmailRequired });
        }
    }
}
