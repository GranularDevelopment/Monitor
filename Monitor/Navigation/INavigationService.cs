using System.Threading.Tasks;
using Xamarin.Forms;

namespace Monitor
{
	public interface INavigationService
	{
		ViewModelBase PreviousPageViewModel { get; }

        Page PreviousPage{ get; }

        Task InitializeAsync();

		Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

		Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

		Task RemoveLastFromBackStackAsync();

		Task RemoveBackStackAsync();

        Task PopAsync();
	}
}
