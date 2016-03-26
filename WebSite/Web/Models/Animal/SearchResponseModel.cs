using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DajLapu.Contracts.Enums;
using DajLapu.Contracts.Types;

namespace DajLapu.Web.Models.Animal
{
    public class SearchResponseModel : ResponseModel
    {
        /// <summary>
        /// маркеры на карте
        /// </summary>
        public class MapInfo
        {
            public int AnimalId { get; set; }

            // icon on map
            public string SmallPhotoUrl { get; set; }

            public Location Location { get; set; }
        }

        /// <summary>
        /// фотки-превьюшки собак
        /// </summary>
        public class ShortInfo
        {
            public int AnimalId { get; set; }

            // preview thumbnail in results list
            public string MediumPhotoUrl { get; set; }

            public AnimalStatusTypes Status { get; set; }
        }

        public int TotalResultsCount { get; set; }

        public IList<MapInfo> MapInfoList { get; set; }

        public IList<ShortInfo> ShortInfoList { get; set; }
    }
}