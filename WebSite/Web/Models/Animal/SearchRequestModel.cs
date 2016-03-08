using System.Collections.Generic;
using DajLapu.Contracts.Enums;

namespace DajLapu.Web.Models.Animal
{
    public class SearchRequestModel
    {
        public AdvertTypes AdvertType { get; set; }

        public AnimalTypes Animal { get; set; }

        public IList<SizeTypes> Sizes { get; set; }

        public IList<SexTypes> Sexes { get; set; }

        public IList<int> ColorIds { get; set; }

        public IList<int> BreedIds { get; set; }

        public IList<AnimalStatusTypes> Statuses { get; set; }
    }
}