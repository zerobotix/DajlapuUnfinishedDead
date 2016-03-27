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

            // todo: заполнить данными из бд
            var model = new AddViewModel
            {
                CatBreeds = Db_GetBreeds(AnimalTypes.Cat),
                CatColors = Db_GetColors(AnimalTypes.Cat),
                DogBreeds = Db_GetBreeds(AnimalTypes.Dog),
                DogColors = Db_GetColors(AnimalTypes.Dog),
                AdvertType = advertType
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult Add(AddRequestModel model)
        {
            // todo: сохранить все данные из модели в бд
            return Json(new ResponseModel { Success = true });
        }

        [HttpGet]
        public ActionResult List()
        {
            // todo: заполнить данными из бд
            var model = new ListViewModel
            {
                CatBreeds = Db_GetBreeds(AnimalTypes.Cat),
                CatColors = Db_GetColors(AnimalTypes.Cat),
                DogBreeds = Db_GetBreeds(AnimalTypes.Dog),
                DogColors = Db_GetColors(AnimalTypes.Dog),
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult List(SearchRequestModel model)
        {
            // todo: заполнить данными из бд в соответствии с данными из model
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

        // todo: заглушка, удалить
        private List<DropdownOption> Db_GetColors(AnimalTypes type)
        {
            return new List<DropdownOption>
            {
                new DropdownOption {Name = "красный" + type, Id = 0},
                new DropdownOption {Name = "синий" + type, Id = 1},
                new DropdownOption {Name = "желтый" + type, Id = 2},
                new DropdownOption {Name = "фиолетовый" + type, Id = 3},
                new DropdownOption {Name = "серебристо-бежевый" + type, Id = 4}
            };
        }

        // todo: заглушка, удалить
        private List<DropdownOption> Db_GetBreeds(AnimalTypes type)
        {
            return new List<DropdownOption>
            {
                new DropdownOption {Name = "лабрадорский гибралтар" + type, Id = 0},
                new DropdownOption {Name = "такса" + type, Id = 1},
                new DropdownOption {Name = "отвратительная порода с очень длинным названием" + type, Id = 2},
                new DropdownOption {Name = "овчарочка" + type, Id = 3},
                new DropdownOption {Name = "котёнок вообще" + type, Id = 4},
                new DropdownOption {Name = "сиамская" + type, Id = 5},
                new DropdownOption {Name = "овчарочка" + type, Id = 6},
                new DropdownOption {Name = "овчарочка" + type, Id = 7},
                new DropdownOption {Name = "овчарочка" + type, Id = 8},
                new DropdownOption {Name = "овчарочка" + type, Id = 9},
                new DropdownOption {Name = "овчарочка" + type, Id = 10},
                new DropdownOption {Name = "овчарочка" + type, Id = 11},
                new DropdownOption {Name = "овчарочка" + type, Id = 12},
                new DropdownOption {Name = "овчарочка" + type, Id = 13},
                new DropdownOption {Name = "овчарочка" + type, Id = 14},
                new DropdownOption {Name = "овчарочка" + type, Id = 15},
                new DropdownOption {Name = "овчарочка" + type, Id = 16},
            };
        }
    }
}