using Hotels.Data;
using Hotels.Infrastructure.Interface;
using Hotels.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Villa_API.Repository.IRepository;

namespace Hotels.Modules.Repository
{
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public RoomTypeRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<RoomType> UpdateAsync(RoomType entity)
        {
            _db.RoomType.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}