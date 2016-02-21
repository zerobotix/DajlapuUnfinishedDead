using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DajLapu.Web.Models.Animal
{
    public class SearchOptionsClientModel
    {
        public string SearchType { get; set; }
        public string AnimalType { get; set; }
        public IList<string> SizeTypes { get; set; }
        public IList<string> ColorTypes { get; set; }
        public IList<string> BreedTypes { get; set; }
        public IList<string> AnimalStatuses { get; set; }
    }
}