using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Monitor
{
    public partial class PaymentWebView : ContentPage
    {
        public PaymentWebView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var vm = BindingContext as PaymentWebViewModel;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
