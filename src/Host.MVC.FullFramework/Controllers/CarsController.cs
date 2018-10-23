using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.FullFramework.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult Index()
        {
            return View();
        }
    }
}