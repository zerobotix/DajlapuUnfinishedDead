using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DajLapu.Contracts.Types;

namespace DajLapu.Web.Models.Animal
{
    public class ListViewModel
    {
        public IList<DropdownOption> Breeds { get; set; }

        public IList<DropdownOption> Colors { get; set; }
    }
}