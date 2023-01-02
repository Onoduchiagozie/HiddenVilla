using Blazored.LocalStorage;
using HiddenVilla_Client;
using HiddenVilla_Client.Service;
using HiddenVilla_Client.Service.Repo;
using Microsoft.AspNetCore.Components.Authorization;
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
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped<IRoomOrderDetailsClient, RoomOrderDetailsClient>();
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) }
                           );
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
