using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Data
{
    public class CartDisplayProvider
    {
        private readonly IFlightDataProvider _flightDataProvider;
        private readonly ICarDataProvider _carDataProvider;
        private readonly IHotelDataProvider _hotelDataProvider;

        public CartDisplayProvider(IFlightDataProvider flightDataProvider, ICarDataProvider carDataProvider, IHotelDataProvider hotelDataProvider)
        {
            _flightDataProvider = flightDataProvider;
            _carDataProvider = carDataProvider;
            _hotelDataProvider = hotelDataProvider;
        }

        public async Task<T> LoadFullCart<T>(CartPersistenceModel cart, CancellationToken cancellationToken) where T: CartModel, new()
        {
            T cartModel = new T()
            {
                CarReservationDuration = cart.CarReservationDuration,
                HotelReservationDuration = cart.HotelReservationDuration
            };

            if (!string.IsNullOrEmpty(cart.DepartingFlight))
            {
                cartModel.DepartingFlight = await _flightDataProvider.FindFlight(cart.DepartingFlight, cancellationToken);
            }

            if (!string.IsNullOrEmpty(cart.ReturningFlight))
            {
                cartModel.ReturningFlight = await _flightDataProvider.FindFlight(cart.ReturningFlight, cancellationToken);
            }

            if (!string.IsNullOrEmpty(cart.HotelReservation))
            {
                cartModel.HotelReservation = await _hotelDataProvider.FindHotel(cart.HotelReservation, cancellationToken);
            }

            if (!string.IsNullOrEmpty(cart.CarReservation))
            {
                cartModel.CarReservation = await _carDataProvider.FindCar(cart.CarReservation, cancellationToken);
            }

            return cartModel;
        }
    }
}
