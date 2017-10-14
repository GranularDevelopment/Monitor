using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class ServerIOView : ContentPage
	{
		LineSeries ioSeries;

		public ServerIOView()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            var  vm = BindingContext as ServerIOViewModel; 

            
            base.OnAppearing();
        }
	}
}
