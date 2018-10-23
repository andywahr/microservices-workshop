using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.FullFramework.Controllers
{
    public class FlightsController : Controller
    {
        // GET: Flights
        public ActionResult Index()
        {
            return View(new SearchModel()
            {
                SearchMode = SearchMode.Flights,
                IncludeEndLocation = true,
                StartLocationLabel = "Depart From",
                EndLocationLabel = "Return From",
                StartDateLabel = "Depart",
                EndDateLabel = "Return"
            });
        }
    }
}