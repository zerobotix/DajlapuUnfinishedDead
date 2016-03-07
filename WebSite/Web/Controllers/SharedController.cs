using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using DajLapu.Contracts.Enums;
using DajLapu.Web.Helpers;

namespace DajLapu.Web.Controllers
{
    public class SharedController : Controller
    {
        /// <summary>
        /// Share specified enum types with javascript code.
        /// Related links:
        /// http://fairwaytech.com/2014/03/making-c-enums-available-within-javascript/
        /// http://stackoverflow.com/questions/6217028/share-constants-between-c-sharp-and-javascript-in-mvc-razor
        /// </summary>
        /// <returns>javascript code with "Enums" variable</returns>
        [OutputCache(Duration = int.MaxValue, Location = OutputCacheLocation.Any)]
        public ContentResult Enums()
        {
            var sharedEnums = new List<Type>
            {
                typeof (AdvertTypes),
                typeof (AnimalTypes),
                typeof (SexTypes),
                typeof (SizeTypes),
                typeof (AnimalStatusTypes)
            };

            var javascript = EnumHelper.GenerateJavaScriptVariable(sharedEnums);
            // output: var Enums = Object.freeze({ ColorTypes: { Red: 1, Green: 2, }, Breeds: { One: 1, Two: 2}, });
            // usage: < script src = "@Url.Action("Enums", "Shared")" ></ script >
            // now we're using t4 templates for the sake of IntelliSense

            return Content(javascript, "text/javascript");
        }

        public ContentResult Constants()
        {
            throw new NotImplementedException();
        }
    }
}