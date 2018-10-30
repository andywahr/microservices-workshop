using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Messages;
using ContosoTravel.Web.Application.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Primitives;
using Newtonsoft.Json;
using Nito.AsyncEx;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ContosoTravel.Web.Application.Services.ServiceBus
{
    public class PurchaseServiceBusService : IPurchaseService
    {
        private readonly AsyncLazy<QueueClient> _serviceBusClient;
        private readonly ContosoConfiguration _contosoConfig;

        public PurchaseServiceBusService(ContosoConfiguration contosoConfig, AzureManagement azureManagement)
        {
            _contosoConfig = contosoConfig;
            _serviceBusClient = new AsyncLazy<QueueClient>(async () =>
            {
                var azure = await azureManagement.ConnectToSubscription(_contosoConfig.SubscriptionId);
                var serviceBusAccount = await azure.ServiceBusNamespaces.GetByResourceGroupAsync(_contosoConfig.ResourceGroupName, _contosoConfig.ServicesMiddlewareAccountName);
                var writeOnlyAuthRule = await serviceBusAccount.AuthorizationRules.GetByNameAsync("WriteOnly");
                string connectionString = writeOnlyAuthRule.GetKeys().PrimaryConnectionString;

                return new QueueClient(connectionString, Constants.QUEUENAME, ReceiveMode.PeekLock, RetryPolicy.Default);

                /*
                 * Still in Preview
                var tokenProvider = TokenProvider.CreateManagedServiceIdentityTokenProvider();
                return new QueueClient($"sb://{Configuration.ServicesMiddlewareAccountName}.servicebus.windows.net/", Constants.QUEUENAME, tokenProvider);
                */
            });
        }

        public async Task<bool> SendForProcessing(string cartId, System.DateTimeOffset PurchasedOn, CancellationToken cancellationToken)
        {
            PurchaseItineraryMessage purchaseItineraryMessage = new PurchaseItineraryMessage() { CartId = cartId, PurchasedOn = PurchasedOn };

            var client = await _serviceBusClient;
            await client.SendAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(purchaseItineraryMessage))));

            return true;
        }
    }
}
