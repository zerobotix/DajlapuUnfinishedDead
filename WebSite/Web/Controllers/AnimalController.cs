using DajLapu.Contracts.Enums;
using DajLapu.Web.Models.Animal;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DajLapu.Web.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet]
        public ActionResult Add(string id)
        {
            var advertType = id.ToLower() == "lost" ? AdvertTypes.Lost : AdvertTypes.Found;

            var model = new // todo: (anonimous objects and dynamics) or AddViewModel?
            {
                Breeds = Db_GetBreeds(),
                Colors = Db_GetColors(),
                AdvertType = advertType
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult Add(AddRequestModel model)
        {
            return Json(new { Success = true });
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = new
            {
                Breeds = Db_GetBreeds(),
                Colors = Db_GetColors(),
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult List(SearchRequestModel model)
        {
            // todo: convert strings to enums into object that should be passed to api request

            var result = new SearchResponseModel();

            for (var i = 0; i < 64; i++)
            {
                var status = AnimalStatusTypes.Empty;
                if (i % 7 == 5) status = AnimalStatusTypes.Dead;
                if (i % 7 == 3) status = AnimalStatusTypes.Sos;
                if (i % 7 == 2) status = AnimalStatusTypes.Housing;

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

        private List<dynamic> Db_GetColors()
        {
            return new List<dynamic>
            {
                new {Name = "красный", Id = 0},
                new {Name = "синий", Id = 1},
                new {Name = "желтый", Id = 2},
                new {Name = "фиолетовый", Id = 3},
                new {Name = "серебристо-бежевый", Id = 4}
            };
        }

        private List<dynamic> Db_GetBreeds()
        {
            return new List<dynamic>
            {
                new {Name = "лабрадорский гибралтар", Id = 0},
                new {Name = "такса", Id = 1},
                new {Name = "отвратительная порода с очень длинным названием", Id = 2},
                new {Name = "овчарочка", Id = 3},
                new {Name = "котёнок вообще", Id = 4},
                new {Name = "сиамская", Id = 5}
            };
        }
    }
}