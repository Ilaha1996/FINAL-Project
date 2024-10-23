using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Newtonsoft.Json;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels;

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

            var request = new RestRequest("auth/login", Method.Post);
            request.AddJsonBody(new
            {
                Username = vm.Username,
                Password = vm.Password,
                RememberMe = vm.RememberMe
            });

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<TokenResponseVM>>(request);

            // Check if the response was successful
            if (response == null || !response.IsSuccessful || response.Data == null)
            {
                var errorMessage = response?.Data?.ErrorMessage ?? "An unexpected error occurred during login.";
                ModelState.AddModelError("", errorMessage);
                return View(vm);
            }

            var data = response.Data.Data;

            // Ensure token is received
            if (data == null)
            {
                ModelState.AddModelError("", "Login failed. Please try again.");
                return View(vm);
            }

            // Append the token to cookies
            HttpContext.Response.Cookies.Append("token", data.AccessToken, new CookieOptions
            {
                Expires = data.ExpireDate,
                HttpOnly = true
            });

            // Redirect to the Appointment Index page after login
            return RedirectToAction("Index", "Appointment");
        }
    }
}
