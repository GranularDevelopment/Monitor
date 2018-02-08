using System;
using System.Threading.Tasks;

namespace GranularMonitorSystem
{
    public class EditServerViewModel: ViewModelBase
    {
        public EditServerViewModel()
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                await Task.FromResult(false);
            }
            catch (Exception e) 
            {
                await DialogService.ShowAlertAsync("Exception", e.ToString(), "OK");

            }
        }
    }
}
