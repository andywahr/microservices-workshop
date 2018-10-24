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
        public CarReservationModel CarReservation { get; set; }
        public HotelReservationModel HotelReservation { get; set; }
    }
}
