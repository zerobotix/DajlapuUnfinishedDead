using System.Collections.Generic;
using DajLapu.Contracts.Enums;
using DajLapu.Contracts.Types;

namespace DajLapu.Web.Models.Animal
{
    public class AddViewModel
    {
        public IList<DropdownOption> Breeds { get; set; } // todo: породы же могут быть котовые и собачьи

        public IList<DropdownOption> Colors { get; set; }

        public AdvertTypes AdvertType { get; set; }
    }
}