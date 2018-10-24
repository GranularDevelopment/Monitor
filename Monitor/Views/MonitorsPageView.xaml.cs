using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Monitor.Models;
using Xamarin.Forms;

namespace Monitor
{
    public partial class MonitorsPageView : ContentPage
    {
        public MonitorsPageView()
        {
            InitializeComponent() ;
            AchievementsContainer.ItemsSource = new ObservableCollection<Achievement>{
                new Achievement{
                    Name = "Xamarin",
                    //Icon = "iconXamarin",
                    Url = "https://docs.microsoft.com/en-us/xamarin/xamarin-forms/"
                },
            };

        }
    }
}
