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
        Task<CartModel> GetCart(string cartId, CancellationToken cancellationToken);
        Task<CartModel> UpsertCartFlights(string cartId, string departingFlightId, string returningFlightId, CancellationToken cancellationToken);
        Task<CartModel> UpsertCartCar(string cartId, string carId, double numberOfDays, CancellationToken cancellationToken);
        Task<CartModel> UpsertCartHotel(string cartId, string hotelId, int numberOfDays, CancellationToken cancellationToken);
    }
}
