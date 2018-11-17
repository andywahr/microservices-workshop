using ContosoTravel.Web.Application.Services;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Data.CosmosSQL
{
    public class CosmosDBProvider
    {
        private AsyncLazy<DocumentClient> _client;
        private ContosoConfiguration _cotosoConfig;

        public CosmosDBProvider(ContosoConfiguration cotosoConfig, AzureManagement azureManagement)
        {
            _cotosoConfig = cotosoConfig;
            _client = new AsyncLazy<DocumentClient>(async () =>
            {
                var azure = await azureManagement.ConnectToSubscription(_cotosoConfig.SubscriptionId);
                var cosmosAccounts = azure.CosmosDBAccounts.List();
                var cosmosAccout = cosmosAccounts.Where(cosmos => cosmos.Name.Equals(_cotosoConfig.DataAccountName, StringComparison.OrdinalIgnoreCase)).First();
                var keys = cosmosAccout.ListKeys();
                return new DocumentClient(new Uri(cosmosAccout.DocumentEndpoint), keys.PrimaryMasterKey, new ConnectionPolicy() { RetryOptions = new RetryOptions() { MaxRetryAttemptsOnThrottledRequests = 20 } });
            });
        }

        public async Task<DocumentClient> GetDocumentClient()
        {
            return await _client;
        }

        public async Task<DocumentClient> GetDocumentClientAndVerifyCollection(string collection, params string[][] indexes)
        {
            // leave indexes for later

            var docClient = await GetDocumentClient();
            await docClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_cotosoConfig.DatabaseName),
                                                                     new DocumentCollection
                                                                     {
                                                                         Id = collection
                                                                     },
                                                                     new RequestOptions { OfferThroughput = 400 });
            return docClient;
        }

        public async Task<T> FindById<T>(DocumentClient client, string collection, string id, CancellationToken cancellationToken)
        {
            return await client.ReadDocumentAsync<T>(UriFactory.CreateDocumentUri(_cotosoConfig.DatabaseName, collection, id), cancellationToken: cancellationToken);
        }

        public async Task<FeedResponse<T>> GetAll<T>(DocumentClient client, string collection, Func<IOrderedQueryable<T>, IQueryable<T>> filter, CancellationToken cancellationToken)
        {
            var query = filter(client.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(_cotosoConfig.DatabaseName, collection),
                                                                                                           new FeedOptions
                                                                                                           {
                                                                                                               EnableCrossPartitionQuery = true
                                                                                                           })).AsDocumentQuery();

            var response = await query.ExecuteNextAsync<T>(cancellationToken);
            return response;
        }

        public async Task<bool> Persist<T>(DocumentClient client, string collection, T instance, CancellationToken cancellationToken)
        {
            await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(_cotosoConfig.DatabaseName, collection), instance, cancellationToken: cancellationToken, disableAutomaticIdGeneration: true);
            return true;
        }
    }
}
