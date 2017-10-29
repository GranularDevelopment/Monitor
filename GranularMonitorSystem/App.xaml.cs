using System;
using System.Threading.Tasks;
using GranularMonitorSystem.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GranularMonitorSystem
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
            ViewModelLocator.RegisterDependencies(false);
        }

		private Task InitNavigation()
		{
			var navigationService = ViewModelLocator.Resolve<INavigationService>();
			return navigationService.InitializeAsync();
		}

        protected override void OnStart()
		{
			// Handle when your app starts
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