using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Monitor
{
    public partial class CustomNavigationView : NavigationPage 
    {
		public CustomNavigationView(): base()
        {
            BackgroundColor= Color.FromHex("#445963");
            BarTextColor = Color.White;
            InitializeComponent();
        }

        public CustomNavigationView(Page root) : base(root)
        {
            BarTextColor = Color.White;
            BackgroundColor= Color.FromHex("#445963");
            InitializeComponent();
        }
    }
}
