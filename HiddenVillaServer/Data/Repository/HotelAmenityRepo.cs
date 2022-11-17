using AutoMapper;
using HiddenVillaServer.Data.Repository.IRepository;
using HiddenVillaServer.Migrations;
using HiddenVillaServer.Model;
using Microsoft.EntityFrameworkCore;
using Sotsera.Blazor.Toaster.Core;


namespace HiddenVillaServer.Data.Repository
{
    public class HotelAmenity : IHotelAmenity
    {
        private readonly VillaDbContext _db;
        public HotelAmenity(VillaDbContext db)
        {
           _db=db;
        }


        public async Task<int> CreateAmenity(HotelAmenity hotelAmenity)
        {
            var addedAmenity = await _db.HotelAmenities.AddAsync(hotelAmenity);
            return await _db.SaveChangesAsync();
            
        }
        
        public async Task<int> DeleteAmenity(int amenityId)
        {
            var roomDetails = await _db.HotelAmenities.FindAsync(amenityId);
            if (roomDetails != null)
            {
                _db.HotelAmenities.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }

        }

        public async Task<IEnumerable<HotelAmenity>> GetAllHotelAmenity()
        {
            try
            {
                IEnumerable<HotelAmenity> hotelAmenities =  await _db.HotelAmenities.ToListAsync();
                return hotelAmenities;
            }
            catch   (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelAmenity> GetAmenity(int amenityId)
        {
            try
            {
                HotelAmenity chosenAmenity = await _db.HotelAmenities.FindAsync(amenityId);
                return chosenAmenity;
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        
        public async Task<HotelAmenity> UpdateAmenity(int amenityId, HotelAmenity hotelAmenityUpdate)
        {
            try
            {
                if (hotelAmenityUpdate!=null)
                {
                    HotelAmenity amenityUpdate=await _db.HotelAmenities.FindAsync(amenityId);
                    var updateAmenity=_db.HotelAmenities.Update(amenityUpdate);
                    await _db.SaveChangesAsync();
                    return updateAmenity.Entity;

                }
                else
                {

                    return null;
                }
            }catch(Exception ex)
            {
                return null;
            }
        }
        

        public Task<HotelAmenity> IsAmenityUnique(string amenityName, int amenityId = 0)
        {
            throw new NotImplementedException();
        }

      
    }
}
