using HiddenVilla_Client.Model;

namespace HiddenVilla_Client.Service.Repo;

public interface IAmenityService
{
    public Task<IEnumerable<HotelAmenity>> GetAmenities();
    public Task<HotelAmenity> GetAmenity(int amenityId);
}