using ContosoTravel.Web.Application.Interfaces.MVC;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Host.MVC.Core.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartController _cartController;

        public CartController(ICartController cartController)
        {
            _cartController = cartController;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await _cartController.Index(cancellationToken));
        }
    }
}