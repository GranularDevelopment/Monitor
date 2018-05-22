using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Monitor
{
    public partial class MasterDetailView : MasterDetailPage 
    {
        public MasterDetailView()
        {
            InitializeComponent();

			MasterViewPage.ListView.ItemSelected += OnItemSelected;
        }

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await ((DashboardViewModel)Dashboardview.BindingContext).InitializeAsync(null);
			
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
				var page = (Page)Activator.CreateInstance(item.TargetType);
				if (page is LoginView)
                {

					await Navigation.PopToRootAsync();
                    await ((page.BindingContext as ViewModelBase)).InitializeAsync(null);
					Application.Current.MainPage = new CustomNavigationView(page);
				}
				else
				{
				    await ((page.BindingContext as ViewModelBase)).InitializeAsync(null);
                    Detail =  new CustomNavigationView(page);
                    MasterViewPage.ListView.SelectedItem = null;
                    IsPresented = false;
				}
            }
        }
    }
}
