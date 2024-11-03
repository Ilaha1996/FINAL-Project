using Microsoft.AspNetCore.Mvc;
using Web_AppointmentSystem.MVC.Services.ExternalServices.Interfaces;
using Web_AppointmentSystem.MVC.Services.Interfaces;
using Web_AppointmentSystem.MVC.UIExceptions;
using Web_AppointmentSystem.MVC.ViewModels.AuthVM;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService /*UserManager<AppUser> userManager, SignInManager<AppUser> signInManager*/)
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

                return RedirectToAction("Index", "Appointment");
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

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            try
            {
                await _authService.ConfirmEmail(email, token);

                TempData["SuccessMessage"] = "Your email has been successfully confirmed. Please log in.";
                return RedirectToAction("Login");
            }
            catch (InvalidTokenException) 
            {
                ModelState.AddModelError("", "The email confirmation link is invalid or has expired.");
                return RedirectToAction("Register"); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Message = "Email confirmation failed. Please try again later or contact support.";
                return View("Common"); 
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _authService.ForgotPassword(vm);
                ViewBag.Message = "A reset password link has been sent to your email.";
                return View("Common");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Message = ex.Message ?? "Password reset failed. Please try again later or contact support.";
                return View("Common");
            }         
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vm)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _authService.ChangePassword(vm);
                TempData["SuccessMessage"] = "Your password has been successfully changed. Please log in with your new password.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Message = ex.Message ?? "Password change failed. Please try again later or contact support.";
                return View("Common");
            }
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _authService.ResetPassword(vm); 
                TempData["SuccessMessage"] = "Your password has been successfully reset. Please log in with your new password.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                ViewBag.Message = ex.Message ?? "Password reset failed. Please try again later or contact support.";
                return View("Common");
            }
        }

    }
}
