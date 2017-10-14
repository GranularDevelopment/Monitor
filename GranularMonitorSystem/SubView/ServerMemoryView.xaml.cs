using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class ServerMemoryView : ContentPage
	{
		public ServerMemoryView()
		{
			InitializeComponent();
		}

        protected async override void OnAppearing()
        {
            this.Content = null;

            var  vm = BindingContext as ServerMemoryViewModel; 

            this.Content = new PlotView
            {
                Model = await vm.CreatePlotModel()
            }; 
            base.OnAppearing();
        }
     }
}
