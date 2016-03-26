using System.Collections.Generic;
using DajLapu.Contracts.Enums;
using DajLapu.Contracts.Types;

namespace DajLapu.Web.Models.Animal
{
    public class AddViewModel
    {
        public IList<DropdownOption> CatBreeds { get; set; }

        public IList<DropdownOption> CatColors { get; set; }

        public IList<DropdownOption> DogBreeds { get; set; }

        public IList<DropdownOption> DogColors { get; set; }

        public AdvertTypes AdvertType { get; set; }
    }
}