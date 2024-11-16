using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.ViewModels;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ReviewVM;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Azure.Core;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestClient _restClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _restClient = new RestClient("https://localhost:7197/api");
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserIdFromToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("reviews", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<ReviewGetVM>>>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                ViewBag.Err = response.Data?.ErrorMessage ?? "An error occurred while fetching reviews.";
                return View(new ReviewPageVM()); 
            }

            var UserId = GetUserIdFromToken();

            var viewModel = new ReviewPageVM
            {
                Reviews = response.Data.Data,
                ReviewCreateVM = new ReviewCreateVM
                {
                    UserId = UserId
                }
            };

            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Index(ReviewPageVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.ReviewCreateVM.UserId = GetUserIdFromToken();

            var token = HttpContext.Request.Cookies["token"];
            var request = new RestRequest("reviews", Method.Post);
            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }
            request.AddJsonBody(model.ReviewCreateVM);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Data?.ErrorMessage ?? "An error occurred.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = GetUserIdFromToken();

            var getRequest = new RestRequest($"Reviews/{id}", Method.Get);
            var getResponse = await _restClient.ExecuteAsync<ApiResponseMessage<ReviewGetVM>>(getRequest);

            if (!getResponse.IsSuccessful || getResponse.Data == null)
            {
                ViewBag.ErrorMessage = "Review not found.";
                return RedirectToAction("Index");
            }

            var review = getResponse.Data.Data;

            if (review.UserId != userId)
            {
                ViewBag.ErrorMessage = "You can only delete your own reviews.";
                return RedirectToAction("Index");
            }

            var token = HttpContext.Request.Cookies["token"];
            var deleteRequest = new RestRequest($"Reviews/{id}", Method.Delete);
            if (!string.IsNullOrEmpty(token))
            {
                deleteRequest.AddHeader("Authorization", $"Bearer {token}");
            }
            var deleteResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(deleteRequest);

            if (!deleteResponse.IsSuccessful)
            {
                ViewBag.ErrorMessage = deleteResponse.Data?.ErrorMessage ?? "Error deleting review.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
