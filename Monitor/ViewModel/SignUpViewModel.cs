using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Monitor.Services.Identity;
using Monitor.Model;
using Monitor.Exceptions;
using Monitor.Services.RequestProvider;
using Monitor.Validation;

namespace Monitor
{
    public class SignUpViewModel:ViewModelBase  
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        private readonly IIdentityService _userService;

        public SignUpViewModel(IIdentityService userService)
        {
            _userService = userService;
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand SignUpCommand => new Command(async () => await SignUpAsync());

        private async Task SignUpAsync()
        {
            User user = new User{
                UserName = _userName.Value,
                Password = _password.Value
            };
            try
            {
                user = await _userService.SignUpAsync<User>(user);
                await DialogService.ShowAlertAsync("You have successfully created an account",user.UserName,"OK");
                Username = new ValidatableObject<string>();
                Password = new ValidatableObject<string>(); 
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

        public ValidatableObject<string> Password
        {
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
            get
            {
                return _password;
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
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }
    }
}
