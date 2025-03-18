using HotelMotor.Modules.Services.Models;

namespace HotelMotor.Modules.Services
{
    public class ServiceService
    {
        private List<Service> _services = new List<Service>();

        public ServiceService()
        {
            _services.Add(new Service { Id = 1, Name = "Reprogramación de ECU", Description = "Modificación del software de la unidad de control del motor para optimizar el rendimiento, aumentar la potencia y mejorar la eficiencia del combustible.", Price = 1500000m });
            _services.Add(new Service { Id = 2, Name = "Filtro de Aire de Alto Flujo", Description = "Un filtro diseñado para permitir un mayor flujo de aire al motor, lo que puede mejorar la potencia y la eficiencia del combustible.", Price = 400000m });
            _services.Add(new Service { Id = 3, Name = "Barras Estabilizadoras Mejoradas", Description = "Instalación de barras estabilizadoras más gruesas para reducir el balanceo de la carrocería en curvas, mejorando la estabilidad y el manejo.", Price = 2000000m });
        }

        public List<Service> GetServices()
        {
            return _services;
        }
    }
}
