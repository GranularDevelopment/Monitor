using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Monitor
{
    public partial class App : Application
    {
		public static bool IsUserLoggedIn = false;
<<<<<<< HEAD
        
        
=======

>>>>>>> master
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
                GlobalSetting.Instance.Mock = true;
            }
            else
            {
                ViewModelLocator.RegisterDependencies(false);
                GlobalSetting.Instance.Mock = false;
            }
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("ios=9ee135fc-f20c-44c3-93ec-531f690ee0fe;" 
                            + "uwp={Your UWP App secret here};" 
                            +"android=92098ef1-a8a3-4e15-8202-d4b0749eb014;",
                            typeof(Analytics), typeof(Crashes));
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
