using HiddenVilla_Client.Model;
using HiddenVillaServer.Model.MetaData;

namespace HiddenVillaServer.Model
{
    public class RoomManagement
    {
        public IEnumerable<HotelImage> Images {get;set;}
        public RoomOrderDetails orderDetails { get;set;}    
        public HotelRoomDTO hotelRoom { get;set;}    
    }
}
