
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Model
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public decimal Stars { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}