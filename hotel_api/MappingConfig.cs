using AutoMapper;
using Hotels.Model;
using Hotels.Modules.Model;
using Hotels.Modules.Models;

namespace Hotels
{
    class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelDto, Hotel>();
            //Booking
            CreateMap<Booking, BookingDto>();
            CreateMap<BookingDto, Booking>();
            //Guest
            CreateMap<Guest, GuestDto>();
            CreateMap<GuestDto, Guest>();
            //Payment
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentDto, Payment>();
            //Room
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
            //Room type
            CreateMap<RoomType, RoomTypeDto>();
            CreateMap<RoomTypeDto, RoomType>();
            //Staff
            CreateMap<Staff, StaffDto>();
            CreateMap<StaffDto,Staff>();
        }
    }
}

