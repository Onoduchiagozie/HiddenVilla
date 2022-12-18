using HiddenVilla_Client.Model;

namespace HiddenVilla_Client.Service.Repo;

public interface IAmenityService
{
    public Task<IEnumerable<HotelClientAmenity>> GetAmenities();
    public Task<HotelClientAmenity> GetAmenity(int amenityId);
}