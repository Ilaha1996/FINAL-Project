using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ReviewVM;

namespace Web_AppointmentSystem.MVC.ViewModels
{
    public class ReviewPageVM
    {
        public ReviewCreateVM ReviewCreateVM { get; set; }
        public List<ReviewGetVM> Reviews { get; set; } = new List<ReviewGetVM>();
    }
}
