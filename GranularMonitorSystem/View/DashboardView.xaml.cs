using System;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class DashboardView : ContentPage
	{
        public DashboardView()
		{
			InitializeComponent();
		}

		protected async override void OnAppearing()
		{
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var vm = BindingContext as DashboardViewModel;
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}
