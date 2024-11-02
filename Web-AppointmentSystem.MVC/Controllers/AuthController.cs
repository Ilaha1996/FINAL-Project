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

        //public async Task<IActionResult> ConfirmEmail(string email, string token)
        //{
        //    var appUser = await _userManager.FindByEmailAsync(email);
        //    if (appUser == null)
        //    {
        //        ViewBag.Message = "User not found";
        //        return View("Common");
        //    }

        //    var result = await _userManager.ConfirmEmailAsync(appUser, token);
        //    if (!result.Succeeded)
        //    {
        //        ViewBag.Message = "Email confirmation failed. The link may have expired or is invalid.";
        //        return View("Common");
        //    }

        //    TempData["SuccessMessage"] = "Your email has been successfully confirmed. Please log in.";
        //    return RedirectToAction("Login");
        //}

        //[HttpGet]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
        //{
        //    if (!ModelState.IsValid) return View();

        //    var appUser = await _userManager.FindByEmailAsync(vm.Email);
        //    if (appUser == null)
        //    {
        //        ModelState.AddModelError("Email", "Email not found");
        //        return View();
        //    }

        //    string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
        //    string resetUrl = Url.Action("ResetPassword", "Account", new { email = vm.Email, token = token }, Request.Scheme);

        //    string emailBody = $"Please reset your password by clicking the following link: <a href='{resetUrl}'>Reset Password</a>";
        //    await _emailService.SendMailAsync(vm.Email, "Password Reset Request", emailBody);

        //    ViewBag.Message = "Reset password link sent to email";
        //    return View("Common");
        //}



    }
}
