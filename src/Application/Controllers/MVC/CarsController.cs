using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Interfaces.MVC;
using ContosoTravel.Web.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Controllers.MVC
{
    public class CarsController : ICarsController
    {
        private readonly ICarDataProvider _carDataProvider;
        private readonly ICartDataProvider _cartDataProvider;
        private readonly IAirportDataProvider _airportDataProvider;
        private readonly ICartCookieProvider _cartCookieProvider;
        private static TimeSpan THREEHOURSBEFOREORAFTER = TimeSpan.FromHours(3);

        public CarsController(ICarDataProvider carDataProvider, ICartDataProvider cartDataProvider, IAirportDataProvider airportDataProvider, ICartCookieProvider cartCookieProvider)
        {
            _carDataProvider = carDataProvider;
            _cartDataProvider = cartDataProvider;
            _airportDataProvider = airportDataProvider;
            _cartCookieProvider = cartCookieProvider;
        }

        public async Task<SearchModel> Index(CancellationToken cancellationToken)
        {
            return new SearchModel()
            {
                SearchMode = SearchMode.Cars,
                IncludeEndLocation = false,
                StartLocationLabel = "Location",
                StartDateLabel = "Pick-Up",
                EndDateLabel = "Drop-Off",
                AirPorts = await _airportDataProvider.GetAll(cancellationToken)
            };
        }

        public async Task<CarReservationModel> Search(SearchModel searchRequest, CancellationToken cancellationToken)
        {
            string cartId = _cartCookieProvider.GetCartCookie();
            if (string.IsNullOrEmpty(cartId))
            {
                CartModel currentCart = new CartModel();
                cartId = currentCart.Id;
                _cartCookieProvider.SetCartCookie(cartId);
            }

            CarReservationModel carReservation = new CarReservationModel() { NumberOfDays = searchRequest.EndDate.Subtract(searchRequest.StartDate).TotalDays };
            carReservation.Cars = await _carDataProvider.FindCars(searchRequest.StartLocation, searchRequest.StartDate, cancellationToken);
            return carReservation;
        }

        public async Task Purchase(CarReservationModel car, CancellationToken cancellationToken)
        {
            string cartId = _cartCookieProvider.GetCartCookie();
            await _cartDataProvider.UpsertCartCar(cartId, car.SelectedCar, car.NumberOfDays, cancellationToken);
        }
    }
}
