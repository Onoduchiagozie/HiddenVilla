using System.Text.Json.Serialization;
using HiddenVilla_Client.Model;
using HiddenVilla_Client.Service.Repo;
using Newtonsoft.Json;
namespace HiddenVilla_Client.Service;

public class AmenityService : IAmenityService
{
    private readonly HttpClient _httpClient;
    public AmenityService(HttpClient client)
    {
        _httpClient = client;
    }
    public async Task<IEnumerable<HotelClientAmenity>> GetAmenities()
    {
        var response =
            await _httpClient.GetAsync($"api/Amenity");
        var content = await response.Content.ReadAsStringAsync();
        var amenity = JsonConvert.DeserializeObject<IEnumerable<HotelClientAmenity>>(content);
        return amenity;
    }

    public Task<HotelClientAmenity> GetAmenity(int amenityId)
    {
        throw new NotImplementedException();
    }
}