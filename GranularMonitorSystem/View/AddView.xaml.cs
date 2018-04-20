using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GranularMonitorSystem
{
    public partial class AddView : ContentPage
    {
        public AddView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var vm = BindingContext as AddViewModel;
            base.OnAppearing();
        }
    }
}
