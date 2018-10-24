using ContosoTravel.Web.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ContosoTravel.Web.Application.Data.Mock
{
    public class AirportDataMockProvider : IAirportDataProvider
    {
        private List<Airport> _airports;
        private Dictionary<string, Airport> _airportLookup;

        public AirportDataMockProvider()
        {
            _airports = Airport.GetAll();
            _airportLookup = _airports.ToDictionary(air => air.AirportCode, air => air);
        }

        public async Task<Airport> FindByCode(string airportCode, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_airportLookup[airportCode]);
        }

        public async Task<IEnumerable<Airport>> GetAll(CancellationToken cancellationToken)
        {
            return await Task.FromResult(_airports);
        }
    }
}
