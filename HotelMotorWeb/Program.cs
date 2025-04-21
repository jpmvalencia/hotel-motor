using HotelMotorWeb;
using HotelMotorWeb.Services.Auth;
using HotelMotorWeb.Services.Orders;
using HotelMotorWeb.Services.Services;
using HotelMotorWeb.Services.Vehicles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7060/api/") });

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<CustomAuthProvider>(); // Para poder acceder a métodos personalizados

builder.Services.AddScoped<VehiclesService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
