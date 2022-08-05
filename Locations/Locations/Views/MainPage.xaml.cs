using System;
using System.Collections.Generic;
using Locations.ViewModels;
using Locations.Models;
using System.Linq;

using Xamarin.Forms;
using Locations.Services;

namespace Locations.Views
{	
	public partial class MainPage : ContentPage
	{
		MainViewModel mainViewModel => BindingContext as MainViewModel;

		public MainPage ()
		{
			InitializeComponent ();

			BindingContext = new MainViewModel(DependencyService.Get<INavService>());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			searchBar.Text = String.Empty;

			mainViewModel?.Init();
		}


        async void searchBar_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
			await DisplayAlert("Search","SearchBar focused!","Ok");
        }
    }
}

