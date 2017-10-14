using System.Threading.Tasks;

namespace GranularMonitorSystem
{
    public interface IDialogService
    {
		Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}
