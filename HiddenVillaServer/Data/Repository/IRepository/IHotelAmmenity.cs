
using HiddenVillaServer.Model;

namespace HiddenVillaServer.Data.Repository.IRepository
{
    public interface IHotelAmenity
    {
        public Task<HotelAmenity> CreateHotelAmenity(HotelAmenity hotelAmenity);
        public Task<HotelAmenity> UpdateHotelAmenity(HotelAmenity hotelAmenity);
        public Task<int> DeleteHotelAmenity(int amenityId);
        public Task<IEnumerable<HotelAmenity>> GetAllHotelAmenity();
        public Task<HotelAmenity> GetHotelAmenity(int amenityId);
        public Task<HotelAmenity> IsSameNameAmenityAlreadyExists(string name);
        

    }
}
