using ContosoTravel.Web.Application.Data.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Interfaces
{
    public interface IAirportDataProvider
    {
        Task<IEnumerable<Airport>> GetAll(CancellationToken cancellationToken);
        Task<Airport> FindByCode(string airportCode, CancellationToken cancellationToken);
    }
}
