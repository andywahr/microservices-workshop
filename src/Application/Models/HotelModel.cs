using ContosoTravel.Web.Application.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application.Models
{
    public class HotelModel
    {
        public HotelModel()
        {
            Id = Guid.NewGuid().ToString("n");
        }

        public string Id { get; set; }
        public string Location { get; set; }

        public AirportModel LocationAirport { get; set; }

        public DateTimeOffset StartingTime { get; set; }
        public int StartingTimeEpoc
        {
            get
            {
                return StartingTime.ToEpoch();
            }
            set
            {

            }
        }

        public DateTimeOffset EndingTime { get; set; }
        public int EndingTimeEpoc
        {
            get
            {
                return EndingTime.ToEpoch();
            }
            set
            {

            }
        }
        public double Cost { get; set; }
        public HotelRoomType RoomType { get; set; }
    }

    public class HotelModelWithPrice
    {
        public HotelModelWithPrice(HotelModel hotel, int numberOfDays)
        {
            Hotel = hotel;
            NumberOfDays = numberOfDays;
        }

        public HotelModel Hotel { get; set; }
        public string TotalPrice => string.Format("{0:c}", Hotel.Cost * NumberOfDays);
        public int NumberOfDays { get; set; }
    }

    public enum HotelRoomType
    {
        King,
        TwoQueens,
        Suite,
        Penthouse,
    }
}
