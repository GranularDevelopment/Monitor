using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Monitor.Exceptions;
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
        private ValidatableObject<string> _password;

        private readonly IIdentityService _userService;

        public ForgotPasswordViewModel(IIdentityService userService)
        {
            _userService = userService;
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand ResetCommand => new Command(async () => await ResetAsync());

        private async Task ResetAsync()
        {
            User user = new User{
                UserName = _userName.Value,
            };
            try
            {
                user = await _userService.ResetAsync<User>(user);
                await DialogService.ShowAlertAsync("An email with instructions to reset your password has been sent to you. "
				                                   ,user.UserName,"OK");
                Username = new ValidatableObject<string>();
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
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
        }
    }
}
