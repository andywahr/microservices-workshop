using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application.Models
{
    public class CartModel
    {
        public CartModel()
        {
            Id = Guid.NewGuid().ToString("n");
        }

        public string Id { get; set; }
        public FlightModel DepartingFlight { get; set; }
        public FlightModel ReturningFlight { get; set; }
        public CarModel CarReservation { get; set; }
        public double CarReservationDuration { get; set; }
        public HotelModel HotelReservation { get; set; }
        public int HotelReservationDuration { get; set; }
        public double TotalCost => (DepartingFlight?.Cost ?? 0d) + (ReturningFlight?.Cost ?? 0d) + (CarReservation?.Cost ?? 0d) * CarReservationDuration + (HotelReservation?.Cost ?? 0d) * HotelReservationDuration;
    }

    public class CartPersistenceModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string DepartingFlight { get; set; }
        public string ReturningFlight { get; set; }
        public string CarReservation { get; set; }
        public double CarReservationDuration { get; set; }
        public string HotelReservation { get; set; }
        public int HotelReservationDuration { get; set; }
    }
}
