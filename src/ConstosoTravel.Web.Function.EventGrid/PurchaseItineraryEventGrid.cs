using Autofac;
using ContosoTravel.Web.Application;
using ContosoTravel.Web.Application.Messages;
using ContosoTravel.Web.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Function.EventGrid
{
    public static class PurchaseItineraryEventGrid
    {
        public static IContainer Container;

        static PurchaseItineraryEventGrid()
        {
            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            Configuration.DataType = (DataType)Enum.Parse(typeof(DataType), config["DataType"]);
            Configuration.ServicesType = (ServicesType)Enum.Parse(typeof(ServicesType), config["ServicesType"]);
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Configuration).Assembly);
            Container = builder.Build();
        }

        [FunctionName("PurchaseItineraryEventGrid")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]PurchaseItineraryMessage req, ILogger log, CancellationToken cancellationToken)
        {
            log.LogInformation($"Starting to finalize purchase of {req.CartId}");

            log.LogDebug("Resolving the FulfillmentServices");
            var fulfillmentService = Container.Resolve<FulfillmentService>();

            log.LogDebug("Calling Purchase method");
            string recordLocator = await fulfillmentService.Purchase(req.CartId, cancellationToken);

            if ( string.IsNullOrEmpty(recordLocator))
            {
                log.LogError($"Finalization purchase of {req.CartId} failed");
                return new BadRequestResult();
            }
            else
            {
                log.LogInformation($"Record Locator {recordLocator} complete for cart {req.CartId}");
                return new OkResult();
            }
        }
    }
}
