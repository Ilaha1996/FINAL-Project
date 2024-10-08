using Web.AppointmentSystem.DATA.DAL;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web.AppointmentSystem.DATA.Repostories;
public class ReviewRepo : GenericRepo<Review>, IReviewRepo
{
    public ReviewRepo(AppDbContext context) : base(context)
    {
    }
}
