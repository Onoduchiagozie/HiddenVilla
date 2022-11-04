using HiddenVillaServer.Model;
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

    }
}
