using Hotels.Data;
using Hotels.Infrastructure.Interface;
using Hotels.Model;
using Villa_API.Repository.IRepository;

namespace Hotels.Modules.Interface
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}