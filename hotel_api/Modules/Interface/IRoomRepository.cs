using Hotels.Infrastructure.Interface;
using Hotels.Model;

namespace Hotels.Modules.Interface
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<Room> UpdateAsync(Room entity);
    }
}