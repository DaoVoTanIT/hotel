using Hotels.Data;
using Hotels.Model;
using Hotels.Modules.Interface;
using Villa_API.Repository.IRepository;

namespace Hotels.Modules.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;

        }

        public async Task<Room> UpdateAsync(Room entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}