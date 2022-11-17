using HiddenVillaServer.Model;
using HiddenVillaServer.Model.MetaData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace HiddenVillaServer.Data
{
    public class VillaDbContext :IdentityDbContext
    {
        public VillaDbContext (DbContextOptions<VillaDbContext> options) : base(options)
        {
        }
       public DbSet<HotelRoom> HotelRooms { get; set; }
       public DbSet<HotelImage> HotelImages { get; set; }
       public DbSet<HotelAmenity> HotelAmmenities { get; set; }
    }
}
