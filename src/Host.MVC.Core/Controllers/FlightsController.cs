using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContosoTravel.Web.Application.Data.Mock;
using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Host.MVC.Core.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightDataProvider _flightDataProvider;
        private readonly ICartDataProvider _cartDataProvider;
        private static TimeSpan THREEHOURSBEFOREORAFTER = TimeSpan.FromHours(3);

        public FlightsController(IFlightDataProvider flightDataProvider, ICartDataProvider cartDataProvider)
        {
            _flightDataProvider = flightDataProvider;
            _cartDataProvider = cartDataProvider;
        }

        public IActionResult Index()
        {
            return View("Search", new SearchModel()
            {
                SearchMode = SearchMode.Flights,
                IncludeEndLocation = true,
                StartLocationLabel = "Depart From",
                EndLocationLabel = "Return From",
                StartDateLabel = "Depart",
                EndDateLabel = "Return",
                AirPorts = Airport.GetAll().OrderBy(air => air.AirportCode)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchModel searchRequest, CancellationToken cancellationToken)
        {
            string cartId = string.Empty;
            if (!Request.Cookies.TryGetValue("CartId", out cartId))
            {
                CartModel currentCart = new CartModel();
                Response.Cookies.Append("CartId", currentCart.Id);
                cartId = currentCart.Id;
            }

            RoundTripModel roundTrip = new RoundTripModel();
            roundTrip.DepartingFlights = await _flightDataProvider.FindFlights(searchRequest.StartLocation, searchRequest.EndLocation, searchRequest.StartDate, THREEHOURSBEFOREORAFTER, cancellationToken);
            roundTrip.ReturningFlights = await _flightDataProvider.FindFlights(searchRequest.EndLocation, searchRequest.StartLocation, searchRequest.EndDate, THREEHOURSBEFOREORAFTER, cancellationToken);
            return View("FlightResults", roundTrip);
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(RoundTripModel searchRequest, CancellationToken cancellationToken)
        {
            string cartId = Request.Cookies["CartId"];

            await _cartDataProvider.UpsertCartFlights(cartId, searchRequest.SelectedDepartingFlight, searchRequest.SelectedReturningFlight, cancellationToken);

            return RedirectToAction("Index", "Cart");
        }
    }
}