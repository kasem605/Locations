using System;
using System.Threading.Tasks;
using Locations.Models;
using Locations.Services;
using Xamarin.Forms;

namespace Locations.ViewModels
{
	public class NewLocationViewModel : BaseValidationViewModel
	{
		string title;

		public string Title
        {
			get => title;

            set
            {
				title = value;
				Validate(() => !string.IsNullOrWhiteSpace(title), "Title is mandatory!");
				OnPropertyChanged();
				SaveCommand.ChangeCanExecute();
            }
        }

		double latitude;

		public double Latitude
		{
			get => latitude;

			set
			{
				latitude = value;
				OnPropertyChanged();
			}
		}

		double longitude;

		public double Longitude
		{
			get => longitude;

			set
			{
				longitude = value;
				OnPropertyChanged();
			}
		}

		DateTime date;

		public DateTime Date
		{
			get => date;

			set
			{
				date = value;
				OnPropertyChanged();
			}
		}

		int favorite;

		public int Favorite
		{
			get => favorite;

			set
			{
				favorite = value;
				Validate(() => favorite >= 1 && favorite <= 5, "Rating must be between 1 & 5");
				OnPropertyChanged();
				SaveCommand.ChangeCanExecute();
			}
		}

		string desc;

		public string Desc
		{
            get => desc;

			set
			{
				desc = value;
				OnPropertyChanged();
			}
		}

		Command saveCommand;

		public Command SaveCommand => saveCommand ?? (saveCommand = new Command(async () => await Save(), CanSave));

		async Task Save()
        {
			if (IsBusy) { return; }

			IsBusy = true;

			try
			{
				LocationEntity newLocation = new LocationEntity
				{
					Title = Title,
					Latitude = Latitude,
					Longitude = Longitude,
					Date = Date,
					Favorite = favorite,
					Desc = Desc
				};

				await Task.Delay(2000);

				await NavService.GoBack();
			}
            finally
            {
				IsBusy = false;
            }
        }

		bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;

		public NewLocationViewModel(INavService navService) : base(navService)
		{
			Date = DateTime.Today;

			Favorite = 1;
		}

        public override void Init()
        {

        }
    }
}

