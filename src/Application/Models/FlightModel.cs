using System;
using System.Collections.Generic;
using System.Text;
using ContosoTravel.Web.Application.Data.Mock;
using Newtonsoft.Json;

namespace ContosoTravel.Web.Application.Models
{
    public class FlightModel
    {
        public FlightModel()
        {
            Id = Guid.NewGuid().ToString("n");
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string DepartingFrom { get; set; }
        public string ArrivingAt { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public int DepartureTimeEpoc
        {
            get
            {
                return DepartureTime.ToEpoch();
            }
            set
            {

            }
        }
        public TimeSpan Duration { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public int ArrivalTimeEpoc
        {
            get
            {
                return ArrivalTime.ToEpoch();
            }
            set
            {

            }
        }

        public AirportModel DepartingFromAiport { get; set; }
        public AirportModel ArrivingAtAiport { get; set; }
        public double Cost { get; set; }
    }
}
