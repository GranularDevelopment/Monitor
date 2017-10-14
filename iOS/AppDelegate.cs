using System;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace GranularMonitorSystem.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Forms.Init();

			OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
