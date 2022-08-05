using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Locations.Models;
using Locations.Services;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Locations.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		bool isLocationSorted = false;
		bool isDateSorted = false;

		string searchText = String.Empty;

		public string SearchText
        {
			get => searchText;

            set
            {
				searchText = value ?? String.Empty;
				OnPropertyChanged("SearchText");

                if (SearchCommand.CanExecute(null))
                {
					searchCommand.Execute(null);
                }
            }
        }
		ObservableCollection<LocationEntity> locations;

		public ObservableCollection<LocationEntity> Locations
        {
			get => locations;
            set
            {
				locations = value;
				OnPropertyChanged();
            }
        }

		IList<LocationEntity> lList = new List<LocationEntity>();

		List<LocationEntity> CreateSampleList()
		{
			lList.Clear();

			lList.Add(
				new LocationEntity
				{
					Title = "Clark Drive",
					Date = new DateTime(2022, 7, 5),
					Desc = "Our Home Street",
					Latitude = 34.067891211330426,
					Longitude = -118.38471265571943,
					Favorite = 5
				});

			lList.Add(
				new LocationEntity
				{
					Title = "Beverly Drive",
					Date = new DateTime(2022, 7, 7),
					Desc = "Our Starbucks Street",
					Latitude = 34.070502,
					Longitude = -118.402110,
					Favorite = 4
				});

			lList.Add(
				new LocationEntity
				{
					Title = "Rodeo Drive",
					Date = new DateTime(2022, 7, 10),
					Desc = "Our Shopping Street",
					Latitude = 34.067796,
					Longitude = -118.401461,
					Favorite = 4
				});

			lList.Add(
				new LocationEntity
				{
					Title = "GreyStone Mansion & Park",
					Date = new DateTime(2022, 7, 12),
					Desc = "Historic Heritage",
					Latitude = 34.091969990409886,
					Longitude = -118.40164467891347,
					Favorite = 3
				});

			lList.Add(
				new LocationEntity
				{
					Title = "Avra Restaurant",
					Date = new DateTime(2022, 7, 14),
					Desc = "Restaurant on Beverly Drive",
					Latitude = 34.068119073606674,	
					Longitude = -118.39988105930165,
					Favorite = 5
				});

			lList.Add(
				new LocationEntity
				{
					Title = "Beverly Hills Park",
					Date = new DateTime(2022, 7, 17),
					Desc = "Nice to take a stroll",
					Latitude = 34.07244189943611,		
					Longitude = -118.40352511065448,
					Favorite = 5
				});

			lList.Add(
				new LocationEntity
				{
					Title = "Beverly Hills Hotel",
					Date = new DateTime(2022, 7, 25),
					Desc = "Historic Hotel",
					Latitude = 34.081977923132484,
					Longitude = -118.41349144796825,
					Favorite = 5
				});
			lList.Add(
				new LocationEntity
				{
					Title = "Spadena House",
					Date = new DateTime(2022, 7, 13),
					Desc = "Known as the Witch's house",
					Latitude = 34.07009807876473,
					Longitude = -118.41083072918688,
					Favorite = 4
				});

			lList.Add(
				new LocationEntity
				{
					Title = "Beverly Hill's City Hall",
					Date = new DateTime(2022, 7, 20),
					Desc = "1930's Landmark",
					Latitude = 34.07325052379168,	
					Longitude = -118.40046826296897,
					Favorite = 4
				});
			return lList.ToList();
		}


		public Command<LocationEntity> ViewCommand => new Command<LocationEntity>(async entity => await NavService.NavigateTo<DetailViewModel, LocationEntity>(entity));

		public Command NewCommand => new Command(async () => await NavService.NavigateTo<NewLocationViewModel>());

		#region SearchBar

		Command searchCommand;

		public Command SearchCommand
        {
			get
            {
				searchCommand = searchCommand ?? new Command(DoSearchCommand,CanExecuteSearchCommand);
				return searchCommand;
            }
        }

		void DoSearchCommand()
        {
			if(SearchText != null)
            {
				Locations = GetSearchResults(SearchText);
				OnPropertyChanged("Locations");
			}
        }

		bool CanExecuteSearchCommand()
        {
			return true;
        }
		#endregion

		public ICommand LocationSortCommand => new Command(() =>
        {
			if (locations.Count > 1)
			{
				if (!isLocationSorted)
				{
					Locations = new ObservableCollection<LocationEntity>(Locations.OrderByDescending(x => x.Title.ToLowerInvariant(), StringComparer.InvariantCulture));
				}
				else
				{
					Locations = new ObservableCollection<LocationEntity>(Locations.OrderBy(x => x.Title.ToLowerInvariant(), StringComparer.InvariantCulture));
				}

				isLocationSorted = !isLocationSorted;
			}
		});

		public ICommand DateSortCommand => new Command(() =>
		{
			if (locations.Count > 1)
			{
				//Locations.Clear();

				if (!isLocationSorted)
				{
					Locations = new ObservableCollection<LocationEntity>(Locations.OrderByDescending(x => x.Date));
				}
				else
				{
					Locations = new ObservableCollection<LocationEntity>(Locations.OrderBy(x => x.Date));
				}

				isLocationSorted = !isLocationSorted;
			}
		});

		ObservableCollection<LocationEntity> GetSearchResults(string query)
        {
			List<LocationEntity> filterList = lList.Where(x => x.Title.ToLowerInvariant().Contains(query.ToLowerInvariant())).ToList();

			Locations.Clear();
			return new ObservableCollection<LocationEntity>(filterList);
        }

        public MainViewModel(INavService navService):base(navService)
        {
			Locations = new ObservableCollection<LocationEntity>();
		}

        public override void Init()
        {
			LoadLocations();
        }

        void LoadLocations()
		{
			Locations.Clear();

			Locations = new ObservableCollection<LocationEntity>(CreateSampleList());
		}
	}
}

