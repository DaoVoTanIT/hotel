using Hotels.Model;

namespace Hotels.Infrastructure.Interface
{
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
        Task<RoomType> UpdateAsync(RoomType entity);

    }
}