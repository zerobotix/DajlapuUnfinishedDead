using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DajLapu.Contracts.Enums;

namespace DajLapu.Web.Models.Animal
{
    public class AddRequestModel
    {
        public AdvertTypes AdvertType { get; set; }

        public AnimalTypes Animal { get; set; }

        public SexTypes Sex { get; set; }

        public SizeTypes Size { get; set; }

        public IList<int> ColorIds { get; set; }

        public IList<int> BreedIds { get; set; }

        public AnimalStatusTypes Status { get; set; }

        public string FoundTime { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public IList<string> Photos { get; set; } // todo: string filename? int id? something other?
    }
}