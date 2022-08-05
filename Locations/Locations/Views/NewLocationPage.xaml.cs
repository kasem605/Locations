using System;
using System.Collections.Generic;
using Locations.ViewModels;
using System.Linq;

using Xamarin.Forms;
using Locations.Services;

namespace Locations.Views
{	
	public partial class NewLocationPage : ContentPage
	{
		NewLocationViewModel newLocationViewModel => BindingContext as NewLocationViewModel;

		public NewLocationPage ()
		{
			InitializeComponent ();
            BindingContextChanged += NewLocationPage_BindingContextChanged;
			BindingContext = new NewLocationViewModel(DependencyService.Get<INavService>());
		}

        private void NewLocationPage_BindingContextChanged(object sender, EventArgs e)
        {
            newLocationViewModel.ErrorsChanged += NewLocationViewModel_ErrorsChanged;
        }

        private void NewLocationViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            bool propHasErrors = (newLocationViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;

            switch (e.PropertyName)
            {
                case nameof(newLocationViewModel.Title):
                    title.LabelColor = propHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(newLocationViewModel.Favorite):
                    favorite.LabelColor = propHasErrors ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}

