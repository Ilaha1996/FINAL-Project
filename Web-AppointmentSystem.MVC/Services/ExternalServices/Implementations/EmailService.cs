using System.Net.Mail;
using System.Net;
using Web_AppointmentSystem.MVC.Services.ExternalServices.Interfaces;

namespace Web_AppointmentSystem.MVC.Services.ExternalServices.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendMailAsync(string to, string subject, string body)
    {
        string fromMail = _configuration.GetSection("EmailService:Mail").Value;
        string password = _configuration.GetSection("EmailService:Password").Value;

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(fromMail, password)
        };

        await client.SendMailAsync(new MailMessage(fromMail, to, subject, body) { IsBodyHtml = true });
    }
}
