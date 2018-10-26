using Autofac;
using ContosoTravel.Web.Application;
using ContosoTravel.Web.Application.Messages;
using ContosoTravel.Web.Application.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Functions.ServiceBus
{
    public static class PurchaseItineraryServiceBus
    {
        public static IContainer Container;

        static PurchaseItineraryServiceBus()
        {
            var config = new ConfigurationBuilder().AddEnvironmentVariables().AddJsonFile("local.settings.json", true).Build();
            Web.Application.Configuration.PopulateFromConfig((name) => config[name]);
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Configuration).Assembly);
            Container = builder.Build();
        }

        [FunctionName("PurchaseItineraryServiceBus")]
        public static async Task Run([ServiceBusTrigger(Constants.QUEUENAME, Connection = "ServiceBusConnection")]string message, ILogger log, CancellationToken cancellationToken)
        {
            PurchaseItineraryMessage purchaseItineraryMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseItineraryMessage>(message);

            log.LogInformation($"Starting to finalize purchase of {purchaseItineraryMessage.CartId}");

            log.LogDebug("Resolving the FulfillmentServices");
            var fulfillmentService = Container.Resolve<FulfillmentService>();

            log.LogDebug("Calling Purchase method");
            string recordLocator = await fulfillmentService.Purchase(purchaseItineraryMessage.CartId, cancellationToken);

            if (string.IsNullOrEmpty(recordLocator))
            {
                log.LogError($"Finalization purchase of {purchaseItineraryMessage.CartId} failed");
            }
            else
            {
                log.LogInformation($"Record Locator {recordLocator} complete for cart {purchaseItineraryMessage.CartId}");
            }
        }
    }
}
