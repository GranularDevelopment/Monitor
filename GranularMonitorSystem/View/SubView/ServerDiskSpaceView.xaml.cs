using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class ServerDiskSpaceView : ContentPage
	{
        #if DEBUG
        private static int counter = 0;
        #endif

        public ServerDiskSpaceView()
		{
            //Debug.WriteLine(String.Format("Creating ServerDiskSpaceView , instances{0}",Interlocked.Increment(ref counter)));
            InitializeComponent();
		}

        #if DEBUG
        ~ServerDiskSpaceView(){
            Debug.WriteLine(String.Format("Removing ServerDiskSpaceView , instances{0}",Interlocked.Increment(ref counter)));
        }
        #endif

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            
        }

	}
}
