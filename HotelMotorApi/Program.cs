using HotelMotorApi.Common;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using HotelMotorApi.Mappings;
using HotelMotorApi.Interfaces;
using HotelMotorApi.Repositories;
using HotelMotorApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


DotNetEnv.Env.Load();

string dbName = Environment.GetEnvironmentVariable("DB_NAME");
string user = Environment.GetEnvironmentVariable("DB_USER");
string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
string connectionString = $"Server=localhost,1433;Database={dbName};User={user};Password={password};TrustServerCertificate=True;";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
// Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//Services
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
