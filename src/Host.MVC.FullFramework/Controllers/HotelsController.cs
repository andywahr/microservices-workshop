using ContosoTravel.Web.Application.Interfaces.MVC;
using ContosoTravel.Web.Application.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.FullFramework.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelsController _hotelController;

        public HotelsController(IHotelsController hotelController)
        {
            _hotelController = hotelController;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            return View("Search", await _hotelController.Index(cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Search(SearchModel searchRequest, CancellationToken cancellationToken)
        {
            return View("HotelResults", await _hotelController.Search(searchRequest, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Purchase(HotelReservationModel hotel, CancellationToken cancellationToken)
        {
            await _hotelController.Purchase(hotel, cancellationToken);
            return RedirectToAction("Index", "Cart");
        }
    }
}