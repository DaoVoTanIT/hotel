using Hotels.Data;
using Hotels.Infrastructure.Interface;
using Hotels.Model;
using Villa_API.Repository.IRepository;

namespace Hotels.Modules.Interface
{
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
        private readonly ApplicationDbContext _db;
        public StaffRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<Staff> UpdateAsync(Staff entity)
        {
            entity.UpdateDate = DateTime.Now;
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}