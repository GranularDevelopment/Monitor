using System;
using System.Collections.Generic;
<<<<<<< HEAD

=======
using System.Collections.ObjectModel;
>>>>>>> master
using Xamarin.Forms;

namespace Monitor
{
    public partial class DashboardView: ContentPage
    {
        public DashboardView()
        {
            InitializeComponent();
<<<<<<< HEAD
        }

		protected override void OnAppearing()
        {

=======
            listView.ItemsSource = new ObservableCollection<MonitorModel>{
            new MonitorModel
            {
                Name = "Very Long Name",
                Description = "Description",
                StatusCode =  "success.png" ,
                Id = 1,
                UserId = 2,
                URL = "https://granulardevelopment.com",
                EmailAlert = true,
                PushAlert = true,
                SMSAlert =true ,
            }};
        }

        protected override void OnAppearing()
        {


>>>>>>> master
            var vm = BindingContext as DashboardViewModel;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
