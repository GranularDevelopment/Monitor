using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Monitor
{
    public partial class ForgotPasswordView : ContentPage
    {
        public ForgotPasswordView()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
        {
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var vm = BindingContext as ForgotPasswordViewModel;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
