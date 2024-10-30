using Microsoft.Extensions.DependencyInjection;
using Web_AppointmentSystem.BUSINESS.Services.ExternalService.Implementation;
using Web_AppointmentSystem.BUSINESS.Services.ExternalService.Interface;
using Web_AppointmentSystem.BUSINESS.Services.Implementations;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;

namespace Web_AppointmentSystem.BUSINESS;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ITimeSlotService, TimeSlotService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
