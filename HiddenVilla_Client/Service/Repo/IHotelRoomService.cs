
using HiddenVilla_Client.Model;

namespace HiddenVilla_Client.Service.Repo;

public interface IHotelRoomService
{
    public Task<IEnumerable<HotelRoomClient>> GetHotelRooms(string CheckinDate, string CheckoutDate);
    public Task<HotelRoomClient> GetHotelRoomDetails(int roomId,string CheckinDate, string CheckoutDate);
}