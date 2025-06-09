using HotelMotorShared.Dtos.OrderDTOs;

namespace HotelMotorApi.Interfaces
{
    public interface IPdfService
    {
        byte[] GeneratePdf(List<OrderDTO> orders);
    }
}
