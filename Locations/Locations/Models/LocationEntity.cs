using System;
namespace Locations.Models
{
	public class LocationEntity
	{
		public string Title { get; set; }
        public double Latitude { get; set; }
		public double Longitude { get; set; }
        public int Favorite { get; set; }
        public DateTime Date { get; set; }
        public string Desc { get; set; }
    }
}

