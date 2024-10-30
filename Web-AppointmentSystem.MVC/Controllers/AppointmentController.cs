using Microsoft.AspNetCore.Mvc;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
