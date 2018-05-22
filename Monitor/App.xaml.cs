using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            //if(Settings.UseMocks)
			if(true)
            {
                ViewModelLocator.RegisterDependencies(true);
                Constants.UseMockServices(true);
            }
            else
            {
                ViewModelLocator.RegisterDependencies(false);
                Constants.UseMockServices(false);
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
