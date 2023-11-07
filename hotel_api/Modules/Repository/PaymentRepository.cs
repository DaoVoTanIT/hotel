using Hotels.Data;
using Hotels.Infrastructure.Interface;
using Hotels.Model;
using Villa_API.Repository.IRepository;

namespace Hotels.Modules.Interface
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext _db;
        public PaymentRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<Payment> UpdateAsync(Payment entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}