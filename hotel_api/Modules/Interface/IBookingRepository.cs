using Hotels.Infrastructure.Interface;
using Hotels.Model;

namespace Hotels.Modules.Interface
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> UpdateAsync(Booking entity);
    }
}