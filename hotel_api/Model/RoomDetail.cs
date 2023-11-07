
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Model
{
    public class RoomDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [ForeignKey("Room")]
        public string RoomID { get; set; }
        public Room? Room { get; set; }
        public decimal Discount { get; set; }
        public decimal Acreage { get; set; }
        public ICollection<string> PictureInfo { get; set; }
        public ICollection<PhysicalFacility>? ListPhysicalFacility { get; set; }

    }
}