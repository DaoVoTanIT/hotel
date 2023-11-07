
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Model
{
    public class PhysicalFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Title { get; set; }
        public bool? IsHighLight { get; set; }
        public ICollection<RoomFacility>? ListPhysicalFacility { get; set; }
    }
}