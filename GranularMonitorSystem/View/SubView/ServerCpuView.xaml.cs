using System;
using System.Collections.Generic;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Xamarin.Forms;
using OxyPlot.Axes;
using OxyPlot.Series;
using GranularMonitorSystem.Common;
using System.Diagnostics;
using System.Threading;

namespace GranularMonitorSystem
{
	public partial class ServerCpuView : ContentPage
	{
        public ServerCpuView()
		{
            InitializeComponent();
		}

        protected  override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            BindingContext = null;
            this.Content = null;
        }
	}
}
