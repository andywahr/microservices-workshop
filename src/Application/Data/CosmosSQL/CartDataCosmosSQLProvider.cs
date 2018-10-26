using ContosoTravel.Web.Application.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ContosoTravel.Web.Application.Models;
using Microsoft.Azure.Documents.Linq;


namespace ContosoTravel.Web.Application.Data.CosmosSQL
{
    public class CartDataCosmosSQLProvider : ICartDataProvider
    {
        private readonly CosmosDBProvider _cosmosDBProvider;
        const string COLLECTIONNAME = "Carts";
        private AsyncLazy<DocumentClient> _getClientAndVerifyCollection;

        public CartDataCosmosSQLProvider(CosmosDBProvider cosmosDBProvider)
        {
            _cosmosDBProvider = cosmosDBProvider;
            _getClientAndVerifyCollection = new AsyncLazy<DocumentClient>(async () =>
            {
                return await _cosmosDBProvider.GetDocumentClientAndVerifyCollection(COLLECTIONNAME, new string[] { "/departingFrom", "/arrivingAt", "/departureTimeEpoc" });
            });
        }

        public async Task<CartPersistenceModel> GetCart(string cartId, CancellationToken cancellationToken)
        {
            var docClient = await _getClientAndVerifyCollection;
            return await _cosmosDBProvider.FindById<CartPersistenceModel>(docClient, COLLECTIONNAME, cartId, cancellationToken);
        }

        public async Task<CartPersistenceModel> UpsertCartFlights(string cartId, string departingFlightId, string returningFlightId, CancellationToken cancellationToken)
        {
            return await UpdateAndPersist(cartId, (cart) =>
            {
                cart.DepartingFlight = departingFlightId;
                cart.ReturningFlight = returningFlightId;
            }, cancellationToken);
        }

        public async Task<CartPersistenceModel> UpsertCartCar(string cartId, string carId, double numberOfDays, CancellationToken cancellationToken)
        {
            return await UpdateAndPersist(cartId, (cart) =>
            {
                cart.CarReservation = carId;
                cart.CarReservationDuration = numberOfDays;
            }, cancellationToken);
        }

        public async Task<CartPersistenceModel> UpsertCartHotel(string cartId, string hotelId, int numberOfDays, CancellationToken cancellationToken)
        {
            return await UpdateAndPersist(cartId, (cart) =>
            {
                cart.HotelReservation = hotelId;
                cart.HotelReservationDuration = numberOfDays;
            }, cancellationToken);
        }

        public async Task DeleteCart(string cartId, CancellationToken cancellationToken)
        {
            var docClient = await _getClientAndVerifyCollection;
            await docClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Configuration.DataAccountName, COLLECTIONNAME, cartId.ToLower()), cancellationToken: cancellationToken);
        }

        private async Task<CartPersistenceModel> UpdateAndPersist(string cartId, Action<CartPersistenceModel> updateMe, CancellationToken cancellationToken)
        {
            var docClient = await _getClientAndVerifyCollection;
            var cart = await GetCart(cartId, cancellationToken);
            updateMe(cart);
            return cart;
        }
    }
}
