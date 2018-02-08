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
