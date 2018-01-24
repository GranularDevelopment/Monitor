using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class WebsiteView : ContentPage
	{

        public WebsiteView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var vm = BindingContext as WebsiteViewModel;
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}
