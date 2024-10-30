using Microsoft.AspNetCore.Mvc;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
