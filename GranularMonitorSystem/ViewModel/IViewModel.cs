using System;
using System.Threading.Tasks;

namespace GranularMonitorSystem
{
	public interface IViewModel
	{
		Task OnLoad();
		void OnEdit();
		Task OnSave();
		Task OnUpdate();
	}
}
