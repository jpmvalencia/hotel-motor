using HotelMotorShared.Models;

namespace HotelMotorApi.Modules.EmailSender.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
