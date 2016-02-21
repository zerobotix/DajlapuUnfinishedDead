using DajLapu.Contracts.Enums;
using DajLapu.Web.Models.Animal;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DajLapu.Web.Controllers
{
    public class AnimalController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(int x)
        {
            return Json(x);
        }

        [HttpGet]
        public ActionResult List()
        {
            //var model = GetFilterOptions();
            return View();
        }

        [HttpPost]
        public JsonResult List(SearchOptionsClientModel searchOptions)
        {
            // todo: convert strings to enums into object that should be passed to api request

            var result = new SearchResultServerModel();

            for (var i = 0; i < 64; i++)
            {
                var status = AnimalStatuses.Empty;
                if (i % 7 == 5) status = AnimalStatuses.Dead;
                if (i % 7 == 3) status = AnimalStatuses.Sos;
                if (i % 7 == 2) status = AnimalStatuses.Housing;

                var shortInfo = new AnimalShortInfo()
                {
                    AnimalStatus = status.ToString().ToLower(),
                    MediumPhotoUrl = @"/Content/samples/dog-" + (i % 21 + 1) + ".png",
                    AnimalId = i
                };

                var mapInfo = new AnimalMapInfo()
                {
                    AnimalId = i,
                    Location = new Location // октябрьская площадь
                    {
                        Latitude = 53.9013769, // 53.969573 - мкад сверху
                        Longitude = 27.5629059 // 27.4188396 - мкад слева 
                    },
                    SmallPhotoUrl = @"/Content/samples/dog-" + (i % 21 + 1) + ".png",
                };

                result.ShortInfoList.Add(shortInfo);
                result.MapInfoList.Add(mapInfo);
            }

            result.TotalResultsCount = 234;

            return Json(result);
        }
    }
}