using HotelMotorShared.DTOs.ServiceDTOs;

namespace HotelMotorApi.Modules.WordGenerator.Interfaces
{
    public interface IWordService
    {
        Task<byte[]> GenerateFileWithServices(List<ServiceDto> services);
    }
}
