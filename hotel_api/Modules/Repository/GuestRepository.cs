using Hotels.Data;
using Hotels.Infrastructure.Interface;
using Hotels.Model;
using Villa_API.Repository.IRepository;

namespace Hotels.Modules.Interface
{
    public class GuestRepository : Repository<Guest>, IGuestRepository
    {
        private readonly ApplicationDbContext _db;
        public GuestRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<Guest> UpdateAsync(Guest entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}