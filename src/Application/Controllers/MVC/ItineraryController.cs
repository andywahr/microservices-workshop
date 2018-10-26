using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Interfaces.MVC;
using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Controllers.MVC
{
    public class ItineraryController : IItineraryController
    {
        private readonly IItineraryDataProvider _itineraryDataProvider;
        private readonly ICartCookieProvider _cartCookieProvider;

        public ItineraryController(IItineraryDataProvider itineraryController, ICartCookieProvider cartCookieProvider)
        {
            _itineraryDataProvider = itineraryController;
            _cartCookieProvider = cartCookieProvider;
        }

        public async Task<ItineraryModel> GetByCartId(CancellationToken cancellationToken)
        {
            string cookieId = _cartCookieProvider.GetCartCookie();
            return await _itineraryDataProvider.FindItinerary(cookieId, cancellationToken);
        }

        public async Task<ItineraryModel> GetByRecordLocator(string recordLocator, CancellationToken cancellationToken)
        {
            return await _itineraryDataProvider.GetItinerary(recordLocator, cancellationToken);
        }
    }
}
