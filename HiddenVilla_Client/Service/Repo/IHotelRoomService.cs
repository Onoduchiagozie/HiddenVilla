
using HiddenVilla_Client.Model;

namespace HiddenVilla_Client.Service.Repo;

public interface IHotelRoomService
{
    public Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string CheckinDate, string CheckoutDate);
    public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId,string CheckinDate, string CheckoutDate);
}