using Xamarin.Forms;

namespace GranularMonitorSystem
{
    public partial class CustomNavigationView : NavigationPage 
    {
        public CustomNavigationView(): base()
        {
            InitializeComponent();
        }

		public CustomNavigationView(Page root) : base(root)
		{
			InitializeComponent();
		}

	}
}
