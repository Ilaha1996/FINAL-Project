namespace Web_AppointmentSystem.BUSINESS.Services.ExternalService.Interface
{
    public interface IEmailService
    {
        Task SendMailAsync(string to, string subject, string body);
    }

}
