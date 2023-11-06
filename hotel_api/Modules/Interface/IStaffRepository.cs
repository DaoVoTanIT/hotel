using Hotels.Infrastructure.Interface;
using Hotels.Model;

namespace Hotels.Modules.Interface
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task<Staff> UpdateAsync(Staff entity);
    }
}