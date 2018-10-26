using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Interfaces
{
    public interface IItineraryDataProvider
    {
        Task<ItineraryModel> FindItinerary(string cartId, CancellationToken cancellationToken);
        Task<ItineraryModel> GetItinerary(string recordLocator, CancellationToken cancellationToken);
        Task UpsertItinerary(ItineraryModel itinerary, CancellationToken cancellationToken);
    }
}
