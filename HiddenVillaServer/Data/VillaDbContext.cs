using HiddenVillaServer.Model;
using HiddenVillaServer.Model.MetaData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HiddenVillaServer.Data
{
    public class VillaDbContext :DbContext
    {
        public VillaDbContext (DbContextOptions<VillaDbContext> options) : base(options)
        {
        }
       public DbSet<HotelRoom> HotelRooms { get; set; }
       public DbSet<HotelImage> HotelImages { get; set; }

    }
}
