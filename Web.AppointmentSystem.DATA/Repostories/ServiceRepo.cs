using Web.AppointmentSystem.DATA.DAL;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA.Repostories;
public class ServiceRepo : GenericRepo<Service>, IServiceRepo
{
    public ServiceRepo(AppDbContext context) : base(context)
    {
    }
}
