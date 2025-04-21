using HotelMotorWeb;
using HotelMotorWeb.Services.Orders;
using HotelMotorWeb.Services.Services;
using HotelMotorWeb.Services.Vehicles;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7060/api/") });

builder.Services.AddScoped<VehiclesService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ServiceService>();

await builder.Build().RunAsync();
