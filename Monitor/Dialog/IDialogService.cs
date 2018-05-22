using System.Threading.Tasks;

namespace Monitor
{
    public interface IDialogService
    {
		Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}
