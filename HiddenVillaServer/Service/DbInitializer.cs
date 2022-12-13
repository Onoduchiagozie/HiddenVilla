using HiddenVillaServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiddenVillaServer.Service;

public class DbInitializer:IDbInitializer
{
    private readonly VillaDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public DbInitializer(VillaDbContext db,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public void Initialize()
    {
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;  
        }
        
        if(_db.Roles.Any(x=>x.Name=="Admin")) return;
        _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(SD.Employee)).GetAwaiter().GetResult();

        _userManager.CreateAsync(new IdentityUser
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            
            EmailConfirmed = true
        },"Admin123*").GetAwaiter().GetResult();

        IdentityUser user = _db.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
        _userManager.AddToRoleAsync(user, SD.Admin).GetAwaiter().GetResult();
    }
}