using Microsoft.EntityFrameworkCore;
using Web.AppointmentSystem.DATA.DAL;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA.Repostories;
public class AppointmentRepo : GenericRepo<Appointment>, IAppointmentRepo
{
    private readonly AppDbContext _context;
    public AppointmentRepo(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsTimeSlotAvailableForServiceAsync(int serviceId, int timeSlotId)
    {
        return !await _context.Appointments.AnyAsync(a => a.ServiceId == serviceId && a.TimeSlotId == timeSlotId);
    }
}
