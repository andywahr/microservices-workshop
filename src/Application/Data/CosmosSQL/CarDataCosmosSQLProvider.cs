using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Data.CosmosSQL
{
    public class CarDataCosmosSQLProvider : IWritableDataProvider<CarModel>, ICarDataProvider
    {
        private readonly CosmosDBProvider _cosmosDBProvider;
        const string COLLECTIONNAME = "Cars";
        private AsyncLazy<DocumentClient> _getClientAndVerifyCollection;

        public CarDataCosmosSQLProvider(CosmosDBProvider cosmosDBProvider)
        {
            _cosmosDBProvider = cosmosDBProvider;
            _getClientAndVerifyCollection = new AsyncLazy<DocumentClient>(async () =>
            {
                return await _cosmosDBProvider.GetDocumentClientAndVerifyCollection(COLLECTIONNAME, new string[] { "/location", "/startingTimeEpoc", "/endingTimeEpoc" });
            });
        }

        public async Task<CarModel> FindCar(string carId, CancellationToken cancellationToken)
        {
            var docClient = await _getClientAndVerifyCollection;
            return await _cosmosDBProvider.FindById<CarModel>(docClient, COLLECTIONNAME, carId, cancellationToken);
        }

        public async Task<IEnumerable<CarModel>> FindCars(string location, DateTimeOffset desiredTime, CancellationToken cancellationToken)
        {
            var docClient = await _getClientAndVerifyCollection;
            return await _cosmosDBProvider.GetAll<CarModel>(docClient, COLLECTIONNAME, (q) => q.Where(c => c.Location == location &&
                                                                                                           c.StartingTimeEpoc >= desiredTime.ToEpoch() &&
                                                                                                           c.EndingTimeEpoc <= desiredTime.ToEpoch()).OrderBy(c => c.Cost), cancellationToken);
        }

        public async Task<bool> Persist(CarModel instance, CancellationToken cancellationToken)
        {
            var docClient = await _getClientAndVerifyCollection;
            return await _cosmosDBProvider.Persist<CarModel>(docClient, COLLECTIONNAME, instance, cancellationToken);
        }
    }
}
