using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application.Models
{
    public class RoundTripModel
    {
        public IEnumerable<FlightModel> DepartingFlights { get; set; }
        public IEnumerable<FlightModel> ReturningFlights { get; set; }
        public string SelectedDepartingFlight { get; set; }
        public string SelectedReturningFlight { get; set; }
    }
}
