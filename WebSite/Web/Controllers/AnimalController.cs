using System;
using DajLapu.Contracts.Enums;
using DajLapu.Web.Models.Animal;
using System.Collections.Generic;
using System.Web.Mvc;
using DajLapu.Contracts.Types;
using DajLapu.Web.Models;

namespace DajLapu.Web.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet]
        public ActionResult Add(string id)
        {
            AdvertTypes advertType;
            if (!Enum.TryParse(id, true, out advertType))
            {
                advertType = AdvertTypes.Found;
            }

            var model = new AddViewModel
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
            return Json(new ResponseModel { Success = true });
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = new ListViewModel
            {
                Breeds = Db_GetBreeds(),
                Colors = Db_GetColors(),
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult List(SearchRequestModel model)
        {
            var shortInfoList = new List<SearchResponseModel.ShortInfo>();
            var mapInfoList = new List<SearchResponseModel.MapInfo>();

            for (var i = 0; i < 64; i++)
            {
                var status = AnimalStatusTypes.Empty;
                if (i % 7 == 5) status = AnimalStatusTypes.Dead;
                if (i % 7 == 3) status = AnimalStatusTypes.Sos;
                if (i % 7 == 2) status = AnimalStatusTypes.Housing;

                var shortInfo = new SearchResponseModel.ShortInfo
                {
                    Status = status,
                    MediumPhotoUrl = @"/Content/samples/dog-" + (i % 21 + 1) + ".png",
                    AnimalId = i
                };

                var mapInfo = new SearchResponseModel.MapInfo
                {
                    AnimalId = i,
                    Location = new Location // октябрьская площадь
                    {
                        Latitude = 53.9013769, // 53.969573 - мкад сверху
                        Longitude = 27.5629059 // 27.4188396 - мкад слева 
                    },
                    SmallPhotoUrl = @"/Content/samples/dog-" + (i % 21 + 1) + ".png",
                };

                shortInfoList.Add(shortInfo);
                mapInfoList.Add(mapInfo);
            }

            var result = new SearchResponseModel
            {
                ShortInfoList = shortInfoList,
                MapInfoList = mapInfoList,
                TotalResultsCount = 234,
                Success = true
            };

            return Json(result);
        }

        private List<DropdownOption> Db_GetColors()
        {
            return new List<DropdownOption>
            {
                new DropdownOption {Name = "красный", Id = 0},
                new DropdownOption {Name = "синий", Id = 1},
                new DropdownOption {Name = "желтый", Id = 2},
                new DropdownOption {Name = "фиолетовый", Id = 3},
                new DropdownOption {Name = "серебристо-бежевый", Id = 4}
            };
        }

        private List<DropdownOption> Db_GetBreeds()
        {
            return new List<DropdownOption>
            {
                new DropdownOption {Name = "лабрадорский гибралтар", Id = 0},
                new DropdownOption {Name = "такса", Id = 1},
                new DropdownOption {Name = "отвратительная порода с очень длинным названием", Id = 2},
                new DropdownOption {Name = "овчарочка", Id = 3},
                new DropdownOption {Name = "котёнок вообще", Id = 4},
                new DropdownOption {Name = "сиамская", Id = 5}
            };
        }
    }
}