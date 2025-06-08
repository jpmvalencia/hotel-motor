using HotelMotorShared.Models;

namespace HotelMotorApi.Modules.EmailSender.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
