using System.Linq.Expressions;
using Hotels.Data;
using Hotels.Model;
using Hotels.Modules.Interface;
using Villa_API.Repository.IRepository;

namespace Hotels.Modules.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly ApplicationDbContext _db;

        public HotelRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<Hotel> UpdateAsync(Hotel entity)
        {
           entity.UpdateDate = DateTime.Now;
           _db.Update(entity);
           await _db.SaveChangesAsync();
           return entity;
        }
    }
}