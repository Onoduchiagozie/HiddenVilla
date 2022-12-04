using System.Text.Json.Serialization;
using HiddenVilla_Client.Model;
using HiddenVilla_Client.Service.Repo;
using Newtonsoft.Json;
namespace HiddenVilla_Client.Service;

public class HotelRoomService:IHotelRoomService
{
    private readonly HttpClient _httpClient;
    public HotelRoomService(HttpClient client)
    {
        _httpClient = client;
    }
    public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string CheckinDate=null, string CheckoutDate=null)
    {
        var response =
            await _httpClient.GetAsync($"api/HotelRoom?CheckinDate={CheckinDate}&CheckoutDate={CheckoutDate}");
        var content = await response.Content.ReadAsStringAsync();
        var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDTO>>(content);
        return rooms;
    }

    public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string CheckinDate, string CheckoutDate)
    {
        throw new NotImplementedException();
    }
}