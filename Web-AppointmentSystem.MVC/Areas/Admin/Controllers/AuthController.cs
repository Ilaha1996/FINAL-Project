using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;

namespace Web_AppointmentSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly RestClient _restClient;
        public AuthController()
        {
            _restClient = new RestClient("https://localhost:7197/api");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(vm);
            }

            var request = new RestRequest("auth", Method.Post);
            request.AddJsonBody(vm);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<LoginResponseVM>>(request);

            if (response == null || !response.IsSuccessful)
            {
                var errorMessage = response?.Data?.ErrorMessage ?? "An unexpected error occurred.";
                ModelState.AddModelError("", errorMessage);
                return View(vm);
            }

            var data = response.Data?.Data;
            if (data == null)
            {
                ModelState.AddModelError("", "Invalid login attempt. Please try again.");
                return View(vm);
            }

            HttpContext.Response.Cookies.Append("token", data.AccessToken, new CookieOptions
            {
                Expires = data.ExpireDate,
                HttpOnly = true
            });

            return RedirectToAction("Index", "Appointment");
        }
    }
}
