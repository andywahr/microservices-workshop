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
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ExecutionContext = Microsoft.Azure.WebJobs.ExecutionContext;

namespace ContosoTravel.Web.Function.EventGrid
{
    public static class PurchaseItineraryEventGrid
    {
        public static IConfiguration _configuration;
        public static Assembly _thisAssembly; 

        static PurchaseItineraryEventGrid()
        {
            _configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            _thisAssembly = typeof(PurchaseItineraryEventGrid).Assembly;
        }

        [FunctionName("PurchaseItineraryEventGrid")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]PurchaseItineraryMessage req, ILogger log, CancellationToken cancellationToken, ExecutionContext context)
        {
            log.LogInformation($"Starting to finalize purchase of {req.CartId}");

            log.LogDebug("Resolving the FulfillmentServices");
            var container = Setup.InitCotosoWithOneTimeLock(_configuration["KeyVaultUrl"], context.FunctionAppDirectory, _thisAssembly);
            var fulfillmentService = container.Resolve<FulfillmentService>();

            log.LogDebug("Calling Purchase method");
            string recordLocator = await fulfillmentService.Purchase(req.CartId, req.PurchasedOn, cancellationToken);

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
