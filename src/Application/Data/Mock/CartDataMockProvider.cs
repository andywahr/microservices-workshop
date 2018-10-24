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
        private readonly ICarDataProvider _carDataProvider;
        private readonly IHotelDataProvider _hotelDataProvider;

        public CartDataMockProvider(IFlightDataProvider flightDataProvider, ICarDataProvider carDataProvider, IHotelDataProvider hotelDataProvider)
        {
            _flightDataProvider = flightDataProvider;
            _carDataProvider = carDataProvider;
            _hotelDataProvider = hotelDataProvider;
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

        public async Task<CartModel> UpsertCartCar(string cartId, string carId, double numberOfDays, CancellationToken cancellationToken)
        {
            CartModel cart;

            if ( !_carts.TryGetValue(cartId, out cart))
            {
                cart = new CartModel();
            }

            cart.CarReservation = await _carDataProvider.FindCar(carId, cancellationToken);
            cart.CarReservationDuration = numberOfDays;
            _carts[cartId] = cart;

            return await Task.FromResult(cart);

        }
        public async Task<CartModel> UpsertCartHotel(string cartId, string hotelId, int numberOfDays, CancellationToken cancellationToken)
        {
            CartModel cart;

            if (!_carts.TryGetValue(cartId, out cart))
            {
                cart = new CartModel();
            }

            cart.HotelReservation = await _hotelDataProvider.FindHotel(hotelId, cancellationToken);
            cart.HotelReservationDuration = numberOfDays;
            _carts[cartId] = cart;

            return await Task.FromResult(cart);

        }
    }
}
