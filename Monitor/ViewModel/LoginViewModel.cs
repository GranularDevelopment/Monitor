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
	public class LoginViewModel:ViewModelBase  
	{
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        private readonly IIdentityService _userService;

        public LoginViewModel(IIdentityService userService)
		{
            _userService = userService;
            _userName = new ValidatableObject<string>();
			_password = new ValidatableObject<string>();

			addValidations();

		}

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand SignInCommand => new Command(async () => await SignInAsync());
        public ICommand SignUpCommand => new Command(async () => await SignUpAsync());
        public ICommand ForgotPasswordCommand => new Command(async () => await ForgotPasswordAsync());
		public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());
        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        private async Task SignInAsync()
        {
            IsBusy = true;

            await LoginAndGetTokenAsync();

            IsBusy = false;
        }

        private async Task SignUpAsync()
        {
            await NavigationService.NavigateToAsync<SignUpViewModel>();
        }

        private async Task ForgotPasswordAsync()
        {
            await NavigationService.NavigateToAsync<ForgotPasswordViewModel>();
        }
        
        public async Task LoginAndGetTokenAsync()
        {
            if(!Validate())
                return;
                
            try
            {
                User user = await _userService.LoginAsync<User>(_userName.Value.Trim(), _password.Value.Trim());
                if(!string.IsNullOrEmpty(user.Token))
                {
                    Settings.AuthAccessToken = user.Token;
					Settings.UserId = user.UserId;
					Settings.UserName = user.UserName;
					await NavigationService.NavigateToAsync<MasterDetailViewModel>();
                    await NavigationService.RemoveLastFromBackStackAsync();
                }
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

		private void addValidations()
		{
			_userName.Validations.Add(new EmailRule<string> { ValidationMessage = "An email-username is required." });
			_password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
		}
	}
}