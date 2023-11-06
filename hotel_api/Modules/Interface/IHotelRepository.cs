using Hotels.Infrastructure.Interface;
using Hotels.Model;

namespace Hotels.Modules.Interface
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<Hotel> UpdateAsync(Hotel entity);
    }
}