using ContosoTravel.Web.Application.Interfaces.MVC;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.FullFramework.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartController _cartController;

        public CartController(ICartController cartController)
        {
            _cartController = cartController;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var cart = await _cartController.Index(cancellationToken);

            if (cart == null)
            {
                return RedirectToAction("Index", "Itinerary");
            }

            return View(cart);
        }


        [HttpPost]
        public async Task<ActionResult> Purchase(CancellationToken cancellationToken)
        {
            if (await _cartController.Purchase(cancellationToken))
            {
                return RedirectToAction("Index", "Itinerary");
            }

            return View("FailedToPurchase");
        }
    }
}