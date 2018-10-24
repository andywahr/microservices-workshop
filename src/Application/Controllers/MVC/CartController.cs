using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Interfaces.MVC;
using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Controllers.MVC
{
    public class CartController : ICartController
    {
        private readonly ICartDataProvider _cartDataProvider;
        private readonly ICartCookieProvider _cartCookieProvider;

        public CartController(ICartDataProvider cartDataProvider, ICartCookieProvider cartCookieProvider)
        {
            _cartDataProvider = cartDataProvider;
            _cartCookieProvider = cartCookieProvider;
        }

        public async Task<CartModel> Index(CancellationToken cancellationToken)
        {
            string cartId = _cartCookieProvider.GetCartCookie();
            return await _cartDataProvider.GetCart(cartId, cancellationToken);
        }
    }
}
