
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Model
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [ForeignKey("Booking")] 
        public string BookingID { get; set; }
        public Booking Booking { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentMethod { get; set; }
    }
}