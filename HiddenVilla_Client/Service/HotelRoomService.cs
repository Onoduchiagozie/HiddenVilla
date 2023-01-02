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
    public async Task<IEnumerable<HotelRoomClient>> GetHotelRooms(string CheckinDate=null, string CheckoutDate=null)
    {
        var response =
            await _httpClient.GetAsync($"api/HotelRoom?CheckinDate={CheckinDate}&CheckoutDate={CheckoutDate}");
        var content =
            await response.Content.ReadAsStringAsync();
        var rooms = 
            JsonConvert.DeserializeObject<IEnumerable<HotelRoomClient>>(content);
        return rooms;
    }

    public async Task<HotelRoomClient> GetHotelRoomDetails(int roomId, string CheckinDate, string CheckoutDate)
    {
        var response =
           await _httpClient.GetAsync($"api/HotelRoom/{roomId}?CheckinDate={CheckinDate}&CheckoutDate={CheckoutDate}");

        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<HotelRoomClient>(content);
            return room;
        }
        else
        {
            var conent =await response.Content.ReadAsStringAsync();
            var errorModel=JsonConvert.DeserializeObject<ErrorModel>(conent);
            throw new Exception(errorModel.ErrorMessage);
        }

    }
}