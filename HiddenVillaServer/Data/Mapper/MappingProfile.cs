using AutoMapper;
using HiddenVillaServer.Model;

namespace HiddenVillaServer.Data.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO,HotelRoom>();
            CreateMap<HotelRoom,HotelRoomDTO>();
        }
    }
}
