using System;
using System.Collections.Generic;
using Locations.Models;
using Locations.Services;
using Locations.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Locations.Views
{	
	public partial class DetailPage : ContentPage
	{
		DetailViewModel detailViewModel => BindingContext as DetailViewModel;

		public DetailPage ()
		{
			InitializeComponent ();
			BindingContext = new DetailViewModel(DependencyService.Get<INavService>());
		}

		void UpdateMap()
        {
			if(detailViewModel.Entity == null)
            {
				return;
            }

			map.MoveToRegion(MapSpan.FromCenterAndRadius(
			new Position(
				detailViewModel.Entity.Latitude,
				detailViewModel.Entity.Longitude),
			Distance.FromKilometers(.1)));

			map.Pins.Add(new Pin
			{
				Type = PinType.Place,
				Label = detailViewModel.Entity.Title,
				Position = new Position(detailViewModel.Entity.Latitude, detailViewModel.Entity.Longitude)
			});
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

			if (detailViewModel != null)
            {
                detailViewModel.PropertyChanged += DetailViewModel_PropertyChanged;
            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

			if (detailViewModel != null)
			{
				detailViewModel.PropertyChanged -= DetailViewModel_PropertyChanged;
			}
		}

        private void DetailViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(DetailViewModel.Entity))
            {
				UpdateMap();
            }
        }
    }
}

