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
        }

		protected override void OnAppearing()
        {
                
            //if (Settings.UseMocks){

            //    listView.ItemsSource = new ObservableCollection<MonitorModel>{
            //    new MonitorModel
            //        {
            //          Name = "Granular",
            //            Description = "Description",
            //            StatusCode =  "success.png" ,
            //            Id = 1,
            //            UserId = 2,
            //            URL = "https://granulardevelopment.com",
            //            EmailAlert = true,
            //            PushAlert = true,
            //            SMSAlert =true ,
            //            }};
                    
            //}

            var vm = BindingContext as DashboardViewModel;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
