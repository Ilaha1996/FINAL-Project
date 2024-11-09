using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.AppointmentSystem.DATA.DAL;
using Web.AppointmentSystem.DATA.Repositories;
using Web.AppointmentSystem.DATA.Repostories;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA;

public static class ServiceRegistration
{

    public static void AddRepostories(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IServiceRepo, ServiceRepo>();
        services.AddScoped<IAppointmentRepo, AppointmentRepo>();
        services.AddScoped<IReviewRepo, ReviewRepo>();

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
    }
}
