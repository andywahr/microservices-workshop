using ContosoTravel.Web.Application.Interfaces.MVC;
using ContosoTravel.Web.Application.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.FullFramework.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsController _carsController;

        public CarsController(ICarsController carsController)
        {
            _carsController = carsController;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            return View("Search", await _carsController.Index(cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Search(SearchModel searchRequest, CancellationToken cancellationToken)
        {
            return View("CarResults", await _carsController.Search(searchRequest, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Purchase(CarReservationModel flight, CancellationToken cancellationToken)
        {
            await _carsController.Purchase(flight, cancellationToken);
            return RedirectToAction("Index", "Cart");
        }
    }
}