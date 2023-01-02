using HiddenVilla_Client.Model;
using HiddenVillaServer.Model;
using HiddenVillaServer.Model.MetaData;
using HiddenVillaServer.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HiddenVillaServer.Data
{
    public class VillaDbContext :IdentityDbContext<IdentityUser>
    {
        public VillaDbContext (DbContextOptions<VillaDbContext> options) : base(options)
        {
        }
       public DbSet<Model.HotelRoom> HotelRooms { get; set; }
       public DbSet<HotelImage> HotelImages { get; set; }
       public DbSet<HotelAmenity> HotelAmmenities { get; set; }
       public DbSet<RoomOrderDetails> RoomOrderingDetails { get; set; }
    }
}
