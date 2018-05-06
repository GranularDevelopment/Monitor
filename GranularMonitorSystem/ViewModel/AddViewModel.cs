using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
    public class AddViewModel:ViewModelBase
    {
        public ICommand ValueChangedCommand => new Command((obj) => SliderChanged(obj));

        private void SliderChanged(object obj)
        {
            long convert = Convert.ToInt64(Math.Round(Convert.ToDouble(obj))); 
        }

        public AddViewModel()
        {
        }

    }
}
