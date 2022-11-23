using System.Collections;
using HiddenVillaServer.Data.Repository.IRepository;
using HiddenVillaServer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace HiddenVillaServer.Data.Repository
{
    public class HotelAmenityRepository : IHotelAmenity
    {
        private readonly VillaDbContext _db;

        public HotelAmenityRepository(VillaDbContext db)
        {
            _db = db;
        }


        public async Task<HotelAmenity> CreateHotelAmenity(HotelAmenity hotelAmenity)
        {
            var addedHotelAmenity = await _db.HotelAmmenities.AddAsync(hotelAmenity);
            await _db.SaveChangesAsync();
            return addedHotelAmenity.Entity;
        }

        public async Task<HotelAmenity> UpdateHotelAmenity( HotelAmenity hotelAmenity)
        {
            if (hotelAmenity != null)
            {
                var updatedamenity = _db.HotelAmmenities.Update(hotelAmenity);
                await _db.SaveChangesAsync();
                return updatedamenity.Entity;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> DeleteHotelAmenity(int amenityId)
        {
            var amenability = await _db.HotelAmmenities.FindAsync(amenityId);
            if (amenability != null)
            {
                var amenityDelete = _db.HotelAmmenities.Remove(amenability);
                return await _db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }

        }

        public async Task<HotelAmenity> GetHotelAmenity(int amenityId)
        {
            try
            {
                HotelAmenity chosenAmenity = await _db.HotelAmmenities.FindAsync(amenityId);
                return chosenAmenity;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<HotelAmenity> IsSameNameAmenityAlreadyExists(string name)
        {
            try
            {
                var amenityDetails =
                    await _db.HotelAmmenities.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim()
                    );
                return amenityDetails;
            }
            catch (Exception ex)
            {

            }
            return new HotelAmenity();
        }

        public async Task<IEnumerable<HotelAmenity>> GetAllHotelAmenity()
        {
            return await _db.HotelAmmenities.ToListAsync();
        }


    }
}



    
