using ContosoTravel.Web.Application.Interfaces.MVC;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContosoTravel.Web.Host.MVC.Core.Controllers
{
    public class ItineraryController : Controller
    {
        private readonly IItineraryController _itineraryController;

        public ItineraryController(IItineraryController itineraryController)
        {
            _itineraryController = itineraryController;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken, string recordLocator = "")
        {
            if (string.IsNullOrEmpty(recordLocator))
            {
                return View(await _itineraryController.GetByCartId(cancellationToken));
            }
            else
            {
                return View(await _itineraryController.GetByRecordLocator(recordLocator, cancellationToken));
            }
        }
    }
}