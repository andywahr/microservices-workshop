using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.FullFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProvider _dataProvider;

        public HomeController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public ActionResult Index()
        {
            ViewData["Title"] = "Index";
            IndexModel model = new IndexModel() { DataProviderName = _dataProvider.GetProvider() };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}