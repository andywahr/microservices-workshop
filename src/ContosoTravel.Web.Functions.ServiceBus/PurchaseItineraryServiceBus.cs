using Autofac;
using ContosoTravel.Web.Application;
using ContosoTravel.Web.Application.Messages;
using ContosoTravel.Web.Application.Services;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Functions.ServiceBus
{
    public static class PurchaseItineraryServiceBus
    {
        public static IConfiguration _configuration;
        public static Assembly _thisAssembly;

        static PurchaseItineraryServiceBus()
        {
            _configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            _thisAssembly = typeof(PurchaseItineraryServiceBus).Assembly;
        }

        [FunctionName("PurchaseItineraryServiceBus")]
        public static async Task Run([ServiceBusTrigger(Constants.QUEUENAME, Connection = "ServiceBusConnection")]string message, ILogger log, CancellationToken cancellationToken, Microsoft.Azure.WebJobs.ExecutionContext context)
        {
            PurchaseItineraryMessage purchaseItineraryMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseItineraryMessage>(message);

            log.LogInformation($"Starting to finalize purchase of {purchaseItineraryMessage.CartId}");

            log.LogDebug("Resolving the FulfillmentServices");
            var container = Setup.InitCotosoWithOneTimeLock(_configuration["KeyVaultUrl"], context.FunctionAppDirectory, _thisAssembly);
            var fulfillmentService = container.Resolve<FulfillmentService>();

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
