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
		
        #if DEBUG
        private static int counter = 0;
        #endif
        public ServerCpuView()
		{
            Debug.WriteLine(String.Format("Creating ServerCpuView, instances{0}",Interlocked.Increment(ref counter)));
            InitializeComponent();
		}

        #if DEBUG
        ~ServerCpuView(){
            
            Debug.WriteLine(String.Format("Removing ServerCpuView, instances{0}",Interlocked.Decrement(ref counter)));
        }

        #endif

        protected async override void OnAppearing()
        {
            var  vm = BindingContext as ServerCpuViewModel; 


            var content = this.Content;
            this.Content = null;

            this.Content = new PlotView
            {
                Model = await vm.CreatePlotModel()
            };

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            BindingContext = null;
            this.Content = null;
        }

	}
}
