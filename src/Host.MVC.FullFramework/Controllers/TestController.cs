using ContosoTravel.Web.Application.Interfaces;
using ContosoTravel.Web.Application.Interfaces.MVC;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.Core.Controllers
{
    public class TestController : Controller
    {
        private readonly IAirportDataProvider _airportDataProvider;

        public TestController(IAirportDataProvider airportDataProvider)
        {
            _airportDataProvider = airportDataProvider;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            return View(ContosoTravel.Web.Application.Models.TestSettings.GetNewTest(await _airportDataProvider.GetAll(cancellationToken)));
        }
    }
}