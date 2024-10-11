using Microsoft.EntityFrameworkCore;
using Web.AppointmentSystem.DATA.DAL;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA.Repostories;
public class TimeSlotRepo : GenericRepo<TimeSlot>, ITimeSlotRepo
{
    private readonly AppDbContext _context;
    public TimeSlotRepo(AppDbContext context) : base(context)
    {
        _context = context;
    }

}
