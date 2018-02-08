using System;
using System.Threading.Tasks;

namespace GranularMonitorSystem
{
    public class EditServerViewModel: ViewModelBase
    {
        public EditServerViewModel()
        {

        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
