using Blazored.LocalStorage;
using HiddenVilla_Client;
using HiddenVilla_Client.Service;
using HiddenVilla_Client.Service.Repo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sotsera.Blazor.Toaster.Core.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddToaster(config =>
{
    //example customizations
    config.PositionClass = Defaults.Classes.Position.TopRight;
    config.PreventDuplicates = true;
    config.NewestOnTop = false;
});

builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) }
                           );
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IRoomOrderDetailsClient, RoomOrderDetailsClient>();  
await builder.Build().RunAsync();
