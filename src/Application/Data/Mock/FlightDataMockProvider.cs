using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Data.Mock
{
    public class FlightDataMockProvider : IFlightDataProvider
    {
        public async Task<IEnumerable<FlightModel>> FindFlights(string departingFrom, string arrivingAt, DateTimeOffset desiredTime, TimeSpan offset, CancellationToken cancellationToken)
        {
            return GetAll().Where(f => f.DepartingFrom.Equals(departingFrom) && f.ArrivingAt.Equals(arrivingAt) &&
                                       f.DepartureTime > desiredTime.Subtract(offset) &&
                                       f.DepartureTime < desiredTime.Add(offset)).OrderByDescending(f => f.DepartureTime);
        }

        public IEnumerable<FlightModel> GetAll()
        {
            Random random = new Random();
            Dictionary<string, Airport> airports = new Dictionary<string, Airport>();
            foreach (var airport in Airport.GetAll())
            {
                airports[airport.AirportCode] = airport;
            }

            List<FlightModel> allFlights = new List<FlightModel>();

            foreach (var flightTime in FlightTime.GetAll())
            {
                for (int dayOffset = 0; dayOffset < 14; dayOffset++)
                {
                    int numberOfFlights = random.Next(5, 20);
                    DateTime today = DateTime.Now.Date.AddDays(dayOffset);
                    TimeZoneInfo departingTimeZone = TimeZoneInfo.FindSystemTimeZoneById(airports[flightTime.DepartingFrom].TimeZone);

                    for (int ii = 0; ii < numberOfFlights; ii++)
                    {
                        int minutesToAddToFiveAm = (int)(random.NextDouble() * 19d * 60d);
                        DateTimeOffset departDate = DateTimeOffset.Parse($"{today.ToString("MM/dd/yyyy")} 5:00 AM {departingTimeZone.BaseUtcOffset.Hours.ToString("00")}:{departingTimeZone.BaseUtcOffset.Minutes.ToString("00")}").AddMinutes(minutesToAddToFiveAm);

                        if (departingTimeZone.IsDaylightSavingTime(today))
                        {
                            departDate = departDate.AddHours(1);
                        }

                        allFlights.Add(new FlightModel()
                        {
                            DepartingFromAiport = airports[flightTime.DepartingFrom],
                            DepartingFrom = flightTime.DepartingFrom,
                            ArrivingAtAiport = airports[flightTime.ArrivingAt],
                            ArrivingAt = flightTime.ArrivingAt,
                            Duration = flightTime.Duration,
                            DepartureTime = departDate,
                            Cost = 1500d * random.NextDouble(),
                            ArrivalTime = CalcLocalTime(flightTime, airports, departDate)

                    });
                    }
                }
            }

            return allFlights;
        }

        private DateTimeOffset CalcLocalTime(FlightTime flightTime, Dictionary<string, Airport> airports, DateTimeOffset departDate)
        {
            DateTimeOffset arrivalTime = departDate.Add(flightTime.Duration);
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(airports[flightTime.ArrivingAt].TimeZone);

            if ( timeZoneInfo.IsDaylightSavingTime(arrivalTime))
            {
                arrivalTime = arrivalTime.AddHours(1);
            }

            return TimeZoneInfo.ConvertTime(arrivalTime.DateTime, timeZoneInfo);
        }
    }
}
