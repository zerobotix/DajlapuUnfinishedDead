using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DajLapu.Controllers
{
    public class AnimalController : Controller
    {
        // GET: Animal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
    }
}