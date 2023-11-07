
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Model
{
    public class RoomFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [ForeignKey("Room")]
        public string RoomId { get; set; }
        public Room Room { get; set; }
        [ForeignKey("PhysicalFacility")]
        public string PhysicalFacilityId { get; set; }
        public PhysicalFacility PhysicalFacility { get; set; }
    }
}