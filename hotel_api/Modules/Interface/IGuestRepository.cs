using Hotels.Infrastructure.Interface;
using Hotels.Model;

namespace Hotels.Modules.Interface
{
    public interface IGuestRepository : IRepository<Guest>
    {
        Task<Guest> UpdateAsync(Guest entity);
    }
}