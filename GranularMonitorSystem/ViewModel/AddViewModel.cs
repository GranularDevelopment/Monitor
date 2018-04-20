using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
    public class AddViewModel:ViewModelBase
    {
        public ICommand ValueChangedCommand => new Command(async(obj) => await SliderChanged(obj));

        private async Task SliderChanged(object obj)
        {
            long convert = Convert.ToInt64(Math.Round(Convert.ToDouble(obj))); 

            Debug.WriteLine(convert);

        }

        public AddViewModel()
        {
        }
    }
}
