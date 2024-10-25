namespace Web_AppointmentSystem.MVC.Services.ExternalServices.Interfaces;

public interface IEmailService
{
    Task SendMailAsync(string to, string subject, string body);
}
