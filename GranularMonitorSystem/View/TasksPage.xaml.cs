using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GranularMonitorSystem
{
	public partial class TasksPage : ContentPage
	{

		public TasksPage(TaskViewModel viewmodel)
		{
			InitializeComponent();
			this.viewmodel = viewmodel;
			BindingContext = viewmodel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//viewmodel.OnLoad();

		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}

			//((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.	}
		}

		TaskViewModel viewmodel;
	}
}
