using Hotels.Infrastructure.Interface;
using Hotels.Model;

namespace Hotels.Modules.Interface
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> UpdateAsync(Payment entity);
    }
}