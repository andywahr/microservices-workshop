using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Data.Mock
{
    public class CartDataMockProvider : ICartDataProvider
    {
        public Dictionary<string, CartModel> _carts = new Dictionary<string, CartModel>();
        private readonly IFlightDataProvider _flightDataProvider;

        public CartDataMockProvider(IFlightDataProvider flightDataProvider)
        {
            _flightDataProvider = flightDataProvider;
        }

        public async Task<CartModel> GetCart(string cartId, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_carts[cartId]);
        }

        public async Task<CartModel> UpsertCartFlights(string cartId, string departingFlightId, string returningFlightId, CancellationToken cancellationToken)
        {
            CartModel cart;

            if ( !_carts.TryGetValue(cartId, out cart))
            {
                cart = new CartModel();
            }

            cart.DepartingFlight = await _flightDataProvider.FindFlight(departingFlightId, cancellationToken);
            cart.ReturningFlight = await _flightDataProvider.FindFlight(returningFlightId, cancellationToken);

            _carts[cartId] = cart;

            return await Task.FromResult(cart);
        }

    }
}
