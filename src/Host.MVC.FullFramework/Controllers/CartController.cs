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
            return View(await _cartController.Index(cancellationToken));
        }
    }
}