using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DajLapu.Contracts.Enums;

namespace DajLapu.Web.Models.Animal
{
    public class Location // alternative to native GeoCoordinate class
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class AnimalMapInfo
    {
        public int AnimalId { get; set; }

        // icon on map
        public string SmallPhotoUrl { get; set; }

        public Location Location { get; set; }
    }

    public class AnimalShortInfo
    {
        public int AnimalId { get; set; }

        // preview thumbnail in results list
        public string MediumPhotoUrl { get; set; }

        public string AnimalStatus { get; set; } // todo: replace string with enum
    }

    public class SearchResultServerModel
    {
        public int TotalResultsCount { get; set; }

        public IList<AnimalMapInfo> MapInfoList { get; set; } = new List<AnimalMapInfo>();

        public IList<AnimalShortInfo> ShortInfoList { get; set; } =  new List<AnimalShortInfo>();
    }
}