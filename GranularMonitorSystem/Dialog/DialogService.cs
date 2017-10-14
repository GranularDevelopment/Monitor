using Acr.UserDialogs;
using System;
using System.Threading.Tasks;


namespace GranularMonitorSystem
{
    public class DialogService: IDialogService 
    {
        public DialogService()
        {
        }

        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
			return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
    }
}
