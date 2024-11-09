using Microsoft.EntityFrameworkCore;
using Web.AppointmentSystem.DATA.DAL;
using Web.AppointmentSystem.DATA.Repostories;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA.Repositories;
public class AppointmentRepo : GenericRepo<Appointment>, IAppointmentRepo
{
    private readonly AppDbContext _context;

    public AppointmentRepo(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsDateTimeAvailableForServiceAsync(int serviceId, DateTime date, TimeSpan startTime)
    {
        return !await _context.Appointments.AnyAsync(a =>
            a.ServiceId == serviceId &&
            a.Date == date &&
            a.StartTime == startTime);
    }
}

