
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Model
{
    public class RoomDetailDto
    {
        public string Id { get; set; }
        public string RoomID { get; set; }
        public decimal Discount { get; set; }
        public decimal Acreage { get; set; }
        public ICollection<string> PictureInfo { get; set; }
        public ICollection<PhysicalFacility>? ListPhysicalFacility { get; set; }
    }
}