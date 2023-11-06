
namespace Hotels.Modules.Models
{
    public class BookingDto
    {
        public string Id { get; set; }
        public string GuestID { get; set; }
        public string RoomID { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }

    }
}