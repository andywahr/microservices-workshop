using ContosoTravel.Web.Application.Data.Mock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoTravel.Web.Application.Models
{
    public class CarModel
    {
        public CarModel()
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
        public CarType CarType { get; set; }
    }

    public class CarModelWithPrice
    {
        public CarModelWithPrice(CarModel car, double numberOfDays)
        {
            Car = car;
            NumberOfDays = numberOfDays;
        }

        public CarModel Car { get; set; }
        public string TotalPrice => string.Format("{0:c}", Car.Cost * NumberOfDays);
        public double NumberOfDays { get; set; }
    }

    public enum CarType
    {
        Compact,
        Intermediate,
        Full,
        SUV,
        Minivan,
        Convertable
    }
}
