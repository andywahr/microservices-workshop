using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoTravel.Web.Host.MVC.Core.Models;
using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;

namespace ContosoTravel.Web.Host.MVC.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProvider _dataProvider;

        public HomeController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IActionResult Index()
        {
            IndexModel model = new IndexModel() { DataProviderName = _dataProvider.GetProvider() };
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
