using System;
using System.Collections.Generic;
using System.Text;
using ContosoTravel.Web.Application.Data.Mock;

namespace ContosoTravel.Web.Application.Models
{
    public class FlightModel
    {
        public FlightModel()
        {
            Id = Guid.NewGuid().ToString("n");
        }

        public string Id { get; set; }
        public string DepartingFrom { get; set; }
        public string ArrivingAt { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }

        public Airport DepartingFromAiport { get; set; }
        public Airport ArrivingAtAiport { get; set; }
        public double Cost { get; set; }
    }
}
