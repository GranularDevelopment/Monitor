﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Monitor
{
    public partial class SignUpView : ContentPage
    {
        public SignUpView()
        {
            InitializeComponent();
        }

	    protected override void OnAppearing()
        {
			var content = this.Content;
			this.Content = null;
            this.Content = content;

            var vm = BindingContext as SignUpViewModel;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
	}
}
