using System;
using System.Collections.Generic;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Xamarin.Forms;
using OxyPlot.Axes;
using OxyPlot.Series;
using GranularMonitorSystem.Common.Utilities;

namespace GranularMonitorSystem
{
	public partial class ServerView: ContentPage
	{

        public ServerView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var vm = BindingContext as ServerViewModel; 
            base.OnAppearing();

		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

	}
}
