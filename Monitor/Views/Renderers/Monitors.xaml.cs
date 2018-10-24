using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Monitor.Renderers
{
    public partial class Monitors :FlexLayout 
    {
        public ICommand EditCommand => new Command(async (sender) => await EditCommandAsync(sender));

        private async Task EditCommandAsync(object sender)
        {
            var s = sender;
        }

        public Monitors()
        {
            InitializeComponent();
        }

       async void click(object sender, EventArgs e){

            //await NavigationService.NavigateToAsync<MonitorViewModel>(sender);
            var s = sender;
            var ec = e;
            DashboardViewModel vm = (base.BindingContext as DashboardViewModel);
        }
    }
}
