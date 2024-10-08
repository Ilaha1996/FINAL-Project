using Web.AppointmentSystem.DATA.DAL;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA.Repostories;
public class AppointmentRepo : GenericRepo<Appointment>, IAppointmentRepo
{
    public AppointmentRepo(AppDbContext context) : base(context)
    {
    }
}
