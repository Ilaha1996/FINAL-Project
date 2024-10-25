using Microsoft.AspNetCore.Mvc;
using Web_AppointmentSystem.MVC.Services.ExternalServices.Interfaces;
using Web_AppointmentSystem.MVC.Services.Interfaces;
using Web_AppointmentSystem.MVC.ViewModels.AuthVM;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                var data = await _authService.Login(vm);

                HttpContext.Response.Cookies.Append("token", data.AccessToken, new CookieOptions
                {
                    Expires = data.ExpireDate,
                    HttpOnly = true
                });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _authService.Register(vm);

                //Teqdimatda ach bunu!!Email servis islemelidi!
                //string bodytemp = $"Dear {vm.Username},Thank you for registering.";
                //await _emailService.SendMailAsync(vm.Email, $"🎉 Welcome, {vm.Username} – Your Registration was Successful!", bodytemp);
               
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public IActionResult Logout()
        {
            _authService.Logout();
            return RedirectToAction("Login");
        }
    }
}
