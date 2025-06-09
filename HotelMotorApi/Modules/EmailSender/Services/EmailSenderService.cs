using HotelMotorApi.Modules.EmailSender.Interfaces;
using HotelMotorShared.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HotelMotorApi.Modules.EmailSender.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailSenderService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName,
                    _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", mailRequest.ToEmail));
                message.Subject = mailRequest.Subject;
                message.Body = new TextPart("html")
                {
                    Text = mailRequest.Body
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpSettings.Server);
                    await client.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            } catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error " + ex);
            }
        }
    }
}
