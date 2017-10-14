using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage(LoginViewModel viewmodel)
		{
			InitializeComponent();
			this.viewmodel = viewmodel;
		}

		async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			//var user = new User()
			//{
			//	UserName = usernameEntry.Text,
			//	Password = passwordEntry.Text,
			//	Email = emailEntry.Text
			//};

			// Sign up logic goes here

			var signUpSucceeded = true;// AreDetailsValid(user);
			if (signUpSucceeded)
			{
				var rootPage = Navigation.NavigationStack.FirstOrDefault();
				if (rootPage != null)
				{
					App.IsUserLoggedIn = true;
                    //DashboardViewModel dbViewModel = new DashboardViewModel();
                    Navigation.InsertPageBefore(new DashboardView(), Navigation.NavigationStack.First());
					await Navigation.PopToRootAsync();
				}
			}
			else
			{
				messageLabel.Text = "Sign up failed";
			}
		}

		//bool AreDetailsValid(IUser user)
		//{
		//	return (!string.IsNullOrWhiteSpace(user.UserName) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"));
		//}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		LoginViewModel viewmodel;

	}
}
