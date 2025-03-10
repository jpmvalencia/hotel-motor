using HotelMotor.Modules.Vehicles.Models;

namespace HotelMotor.Modules.Vehicles
{
    public class VehicleService
    {
        private List<Vehicle> _vehicles = new List<Vehicle>();
        private int _nextId = 1;

        public List<Vehicle> GetVehicles()
        {
            return _vehicles;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicle.Id = _nextId++;
            _vehicles.Add(vehicle);
        }
    }
}
