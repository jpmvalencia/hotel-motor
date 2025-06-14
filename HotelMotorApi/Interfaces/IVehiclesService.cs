﻿using HotelMotorShared.Dtos;
using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IVehiclesService
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<IEnumerable<Vehicle>> GetVehiclesByCustomerIdAsync(int customerId);
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<Customer?> GetCustomerByVehicleIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByVehicleIdAsync(int vehicleId);
        Task<Vehicle> AddVehicleAsync(AddVehicleDTO addVehicleDto);
        Task<Vehicle> UpdateVehicleAsync(int id, UpdateVehicleDTO updateVehicleDto);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
