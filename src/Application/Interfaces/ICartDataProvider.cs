using ContosoTravel.Web.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoTravel.Web.Application.Interfaces
{
    public interface ICartDataProvider
    {
        Task<CartPersistenceModel> GetCart(string cartId, CancellationToken cancellationToken);
        Task<CartPersistenceModel> UpsertCartFlights(string cartId, string departingFlightId, string returningFlightId, CancellationToken cancellationToken);
        Task<CartPersistenceModel> UpsertCartCar(string cartId, string carId, double numberOfDays, CancellationToken cancellationToken);
        Task<CartPersistenceModel> UpsertCartHotel(string cartId, string hotelId, int numberOfDays, CancellationToken cancellationToken);
        Task DeleteCart(string cartId, CancellationToken cancellationToken);
    }
}
