using System.Threading.Tasks;


namespace Monitor
{
    public abstract class ViewModelBase : ExtendedBindableObject 
	{
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

		protected ViewModelBase()
		{
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
            //GlobalSetting.Instance.BaseEndpoint = Settings.UrlBase;
		}

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

	}
}
