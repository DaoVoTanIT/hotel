
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Model
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [ForeignKey("Hotel")]
        public string HotelId { get; set; }
        public Hotel Hotel { get; set; }
        [ForeignKey("Type")]
        public string TypeId { get; set; }
        public RoomType Type { get; set; }
        public string Status { get; set; }
    }
}