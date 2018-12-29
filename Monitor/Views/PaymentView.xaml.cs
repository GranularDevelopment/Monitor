using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Monitor
{
    public partial class PaymentView : ContentPage
    {
        public PaymentView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var vm = BindingContext as PaymentViewModel;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
