﻿using ContosoTravel.Web.Application.Interfaces.MVC;
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
            var cart = await _cartController.Index(cancellationToken);

            if (cart == null)
            {
                return RedirectToAction("Index", "Itinerary");
            }

            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> Purchase(CancellationToken cancellationToken)
        {
            if (await _cartController.Purchase(cancellationToken))
            {
                return RedirectToAction("Index", "Itinerary");
            }

            return View("FailedToPurchase");
        }
    }
}