using Web.AppointmentSystem.DATA.DAL;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA.Repostories;
public class TimeSlotRepo : GenericRepo<TimeSlot>, ITimeSlotRepo
{
    public TimeSlotRepo(AppDbContext context) : base(context)
    {
    }
}
