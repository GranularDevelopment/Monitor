using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class DashboardView : ContentPage
	{
        public static ObservableCollection<string> items { get; set; }

        public DashboardView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
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
