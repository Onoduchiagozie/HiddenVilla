using HiddenVillaServer.Data;
using HiddenVillaServer.Data.Repository;
using HiddenVillaServer.Data.Repository.IRepository;
using HiddenVillaServer.Model;
using HiddenVillaServer.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Sotsera.Blazor.Toaster.Core.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<VillaDbContext>(options =>
                        options.UseSqlServer(builder.Configuration
                        .GetConnectionString("DefaultConnection"))
                        );

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<VillaDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IHotelRoomRepo,HotelRoomRepo>();
builder.Services.AddScoped<IHotelImagesRepository,HotelImagesRepository>();
builder.Services.AddScoped<IHotelAmenity,HotelAmenityRepository>();
builder.Services.AddScoped<IFileUpload,FileUpload>();
builder.Services.AddToaster(config =>
{
    //example customizations
    config.PositionClass = Defaults.Classes.Position.TopRight;
    config.PreventDuplicates = true;
    config.NewestOnTop = false;
});

builder.Services.AddHttpContextAccessor();
//builder.Services.AddToastr(new ToastrOptions { closeButton = true, hideDuration = 3000 });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();    
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
