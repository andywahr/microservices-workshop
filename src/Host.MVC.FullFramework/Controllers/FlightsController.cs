using ContosoTravel.Web.Application.Interfaces.MVC;
using ContosoTravel.Web.Application.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.FullFramework.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightsController _flightsController;

        public FlightsController(IFlightsController flightController)
        {
            _flightsController = flightController;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            return View("Search", await _flightsController.Index(cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Search(SearchModel searchRequest, CancellationToken cancellationToken)
        {
            return View("FlightResults", await _flightsController.Search(searchRequest, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Purchase(FlightReservationModel flight, CancellationToken cancellationToken)
        {
            await _flightsController.Purchase(flight, cancellationToken);
            return RedirectToAction("Index", "Cart");
        }
    }
}