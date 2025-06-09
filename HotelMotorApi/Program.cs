using HotelMotorApi.Common;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using HotelMotorApi.Mappings;
using HotelMotorApi.Interfaces;
using HotelMotorApi.Repositories;
using HotelMotorApi.Services;
using FluentValidation;
using HotelMotorApi.Validators.Customer;
using HotelMotorShared.Dtos.CustomerDTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7078").AllowAnyHeader().AllowAnyMethod();
    });
});

string jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

Console.WriteLine($"JWT_KEY: {jwtKey}");
Console.WriteLine($"JWT_ISSUER: {jwtIssuer}");
Console.WriteLine($"JWT_AUDIENCE: {jwtAudience}");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string dbName = Environment.GetEnvironmentVariable("DB_NAME");
string user = Environment.GetEnvironmentVariable("DB_USER");
string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
string connectionString = $"Server=localhost,1433;Database={dbName};User={user};Password={password};TrustServerCertificate=True;";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null);
}));

// Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IVehiclesRepository, VehiclesRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IVehiclesService, VehiclesService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Validators
builder.Services.AddScoped<IValidator<CustomerCreateDto>, CustomerCreateValidator>();
builder.Services.AddScoped<IValidator<CustomerUpdateDto>, CustomerUpdateValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorClient");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
