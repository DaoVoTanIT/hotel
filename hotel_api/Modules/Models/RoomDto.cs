

namespace Hotels.Modules.Models
{
    public class RoomDto
    {
        public string Id { get; set; }
        public string HotelId { get; set; }
        public string TypeId { get; set; }
        public string Status { get; set; }
        public ICollection<PhysicalFacilityDto>? ListPhysicalFacility { get; set; }

    }
}