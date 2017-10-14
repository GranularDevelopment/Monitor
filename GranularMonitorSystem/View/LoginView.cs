using System;
using System.Collections.Generic;
using GranularMonitorSystem.Common;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class LoginView : ContentPage
	{
        public LoginView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var vm = BindingContext as LoginViewModel;

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}
