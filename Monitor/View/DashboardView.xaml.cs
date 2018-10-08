using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Monitor
{
    public partial class DashboardView: ContentPage
    {
        public DashboardView()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
        {

            var vm = BindingContext as DashboardViewModel;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
