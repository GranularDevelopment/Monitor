using System.Threading.Tasks;

namespace GranularMonitorSystem
{
	public interface INavigationService
	{
		ViewModelBase PreviousPageViewModel { get; }

		Task InitializeAsync();

		Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

		Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

		Task RemoveLastFromBackStackAsync();

		Task RemoveBackStackAsync();
	}
}
