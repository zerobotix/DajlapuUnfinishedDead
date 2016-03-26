using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DajLapu.Contracts.Types;

namespace DajLapu.Web.Models.Animal
{
    public class ListViewModel
    {
        public IList<DropdownOption> CatBreeds { get; set; }

        public IList<DropdownOption> CatColors { get; set; }

        public IList<DropdownOption> DogBreeds { get; set; }

        public IList<DropdownOption> DogColors { get; set; }
    }
}