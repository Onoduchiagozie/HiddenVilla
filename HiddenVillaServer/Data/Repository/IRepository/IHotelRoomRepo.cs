using HiddenVillaServer.Model;

namespace HiddenVillaServer.Data.Repository.IRepository
{
    public interface IHotelRoomRepo
    {
        public Task<HotelRoomDTO>  CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO>  GetHotelRoom(int roomId,string checkinDate=null,string CheckoutDate=null);
        public Task<IEnumerable<HotelRoomDTO>>  GetAllHotelRooms(string checkinDate=null,string CheckoutDate=null);
        public Task<HotelRoomDTO> IsRoomUnique(string hotelname,int roomId=0);
        public Task<int> DeleteHotel(int roomId);

    }
}
