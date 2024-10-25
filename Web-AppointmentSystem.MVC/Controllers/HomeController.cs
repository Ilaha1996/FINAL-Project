using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_AppointmentSystem.MVC.Models;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class HomeController : Controller
    {    
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            return View();
        }      
    }
}
