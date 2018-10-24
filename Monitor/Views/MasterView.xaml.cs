using System;
using Xamarin.Forms;

namespace Monitor
{
    public partial class MasterView: ContentPage 
	{
		public ListView ListView { get { return listView; } }

        public MasterView() 
		{ 
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            var content = this.Content;
            this.Content = null;
            this.Content = content;
            
			var vm = BindingContext = new MasterViewModel();

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}