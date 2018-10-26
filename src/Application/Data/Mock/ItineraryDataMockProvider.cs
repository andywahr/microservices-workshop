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
    public class ItineraryDataMockProvider : IItineraryDataProvider
    {
        public Dictionary<string, ItineraryModel> _itineraries = new Dictionary<string, ItineraryModel>();

        public async Task<ItineraryModel> FindItinerary(string cartId, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_itineraries.Values.FirstOrDefault(itinerary => itinerary.Id.Equals(cartId, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<ItineraryModel> GetItinerary(string recordLocator, CancellationToken cancellationToken)
        {
            ItineraryModel _itinerary;
            _itineraries.TryGetValue(recordLocator, out _itinerary);
            return await Task.FromResult(_itinerary);
        }

        public async Task UpsertItinerary(ItineraryModel itinerary, CancellationToken cancellationToken)
        {
            _itineraries[itinerary.RecordLocator] = itinerary;
            await Task.Delay(0);
        }
    }
}
