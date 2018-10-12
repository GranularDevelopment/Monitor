using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Monitor
{
    public partial class DashboardView: ContentPage
    {
        public DashboardView()
        {
            InitializeComponent();
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


            var vm = BindingContext as DashboardViewModel;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
