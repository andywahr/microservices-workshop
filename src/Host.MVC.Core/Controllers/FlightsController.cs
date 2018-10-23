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
    public class FlightsController : Controller
    {
        private readonly IFlightDataProvider _flightDataProvider;
        private static TimeSpan THREEHOURSBEFOREORAFTER = TimeSpan.FromHours(3);

        public FlightsController(IFlightDataProvider flightDataProvider)
        {
            _flightDataProvider = flightDataProvider;
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
                EndDateLabel = "Return"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchModel searchRequest, CancellationToken cancellationToken)
        {
            RoundTripModel roundTrip = new RoundTripModel();
            roundTrip.DepartingFlights = await _flightDataProvider.FindFlights(searchRequest.StartLocation, searchRequest.EndLocation, searchRequest.StartDate, THREEHOURSBEFOREORAFTER, cancellationToken);
            roundTrip.ReturningFlights = await _flightDataProvider.FindFlights(searchRequest.EndLocation, searchRequest.StartLocation, searchRequest.EndDate, THREEHOURSBEFOREORAFTER, cancellationToken);
            return View("FlightResults", roundTrip);
        }
    }
}