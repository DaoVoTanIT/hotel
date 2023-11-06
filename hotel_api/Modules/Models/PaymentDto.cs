

namespace Hotels.Modules.Model
{
    public class PaymentDto
    {
        public string Id { get; set; }
        public string BookingID { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentMethod { get; set; }
    }
}