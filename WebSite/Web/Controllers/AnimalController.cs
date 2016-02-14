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
        public JsonResult List(object x)
        {
            return Json(x);
        }
    }
}