using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Monitor
{
    public partial class App : Application
    {
		public static bool IsUserLoggedIn = false;

		public App()
        {
            IsUserLoggedIn = false; 

            InitializeComponent();

            InitApp();
            InitNavigation();
        }

        private void InitApp()
        {
            if(Settings.UseMocks)
            {
                ViewModelLocator.RegisterDependencies(true);
                GlobalRoutingSettings.Instance.Mock = true;
            }
            else
            {
                ViewModelLocator.RegisterDependencies(false);
                GlobalRoutingSettings.Instance.Mock = false;
            }
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected async override void OnStart()
        {

            //if (!AppCenter.Configured)
            //{
            //    Push.PushNotificationReceived += (sender, e) =>
            //    {
            //        // Add the notification message and title to the message
            //        var summary = $"Push notification received:" +
            //            $"\n\tNotification title: {e.Title}" +
            //            $"\n\tMessage: {e.Message}";

            //        // If there is custom data associated with the notification,
            //        // print the entries
            //        if (e.CustomData != null)
            //        {
            //            summary += "\n\tCustom data:\n";
            //            foreach (var key in e.CustomData.Keys)
            //            {
            //                summary += $"\t\t{key} : {e.CustomData[key]}\n";
            //            }
            //        }

            //        // Send the notification summary to debug output
            //        System.Diagnostics.Debug.WriteLine(summary);
            //    };
            //}

            AppCenter.Start(Settings.iOSAppCenterPush +
                           Settings.AndroidAppCenterPush, typeof(Push));
            // Handle when your app starts
            AppCenter.Start(Settings.iOSAppCenterCrash +
                            Settings.AndroidAppCenterCrash,
                            typeof(Analytics), typeof(Crashes));

            Settings.DeviceGuid = (Guid)await AppCenter.GetInstallIdAsync();

        }

      

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
