using System;
using Locations.Models;
using Locations.Services;

namespace Locations.ViewModels
{
	public class DetailViewModel : BaseViewModel<LocationEntity>
	{
		LocationEntity entity;

		public LocationEntity Entity
        {
			get => entity;

            set
            {
				entity = value;
				OnPropertyChanged();
            }
        }

		public DetailViewModel(INavService navService):base(navService)

        {

		}

        public override void Init(LocationEntity entity)
        {
            Entity = entity;
        }
    }
}

