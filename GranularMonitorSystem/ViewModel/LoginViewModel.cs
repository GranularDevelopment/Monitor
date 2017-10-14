using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GranularMonitorSystem.Common;
using Xamarin.Forms;
using GranularMonitorSystem.Services.API.Identity;
using GranularMonitorSystem.Model;

namespace GranularMonitorSystem
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
		}

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand SignInCommand => new Command(async () => await SignInAsync());

        private async Task SignInAsync()
        {
            IsBusy = true;

            await Task.Delay(500);
            await LoginAndGetToken();

            IsBusy = false;
        }

        public async Task LoginAndGetToken()
        {
            if(!Validate())
                return;
            
            User user = await _userService.LoginAsync<User>(_userName.Value, _password.Value);
            if(!string.IsNullOrEmpty(user.Token))
            {
                Constants.TOKEN = user.Token;
                await NavigationService.NavigateToAsync<DashboardViewModel>();
                await NavigationService.RemoveLastFromBackStackAsync();
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
