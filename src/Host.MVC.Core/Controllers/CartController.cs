using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Host.MVC.Core.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartDataProvider _cartDataProvider;
        private static TimeSpan THREEHOURSBEFOREORAFTER = TimeSpan.FromHours(3);

        public CartController(ICartDataProvider cartDataProvider)
        {
            _cartDataProvider = cartDataProvider;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            string cartId = string.Empty;
            if (!Request.Cookies.TryGetValue("CartId", out cartId))
            {
                RedirectToAction();
            }

            CartModel currentCart = await _cartDataProvider.GetCart(cartId, cancellationToken);

            return View(currentCart);
        }
    }
}