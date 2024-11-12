using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.ViewModels;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ReviewVM;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            var request = new RestRequest("Reviews", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<ReviewGetVM>>>(request);

            if (!response.IsSuccessful || response.Data == null)
            {
                ViewBag.Err = response.Data?.ErrorMessage ?? "An error occurred while fetching reviews.";
                return View(new ReviewPageVM()); 
            }

            var viewModel = new ReviewPageVM
            {
                Reviews = response.Data.Data
            };

            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Index(ReviewCreateVM model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "You need to log in to write a review.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return await Index(); 
            }

            var request = new RestRequest("Reviews", Method.Post);
            request.AddJsonBody(model);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<ReviewGetVM>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.ErrorMessage = response.Data?.ErrorMessage ?? "Error creating review.";
                return await Index();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "You need to log in to delete a review.";
                return RedirectToAction("Index");
            }

            var getRequest = new RestRequest($"Reviews/{id}", Method.Get);
            var getResponse = await _restClient.ExecuteAsync<ApiResponseMessage<ReviewGetVM>>(getRequest);

            if (getResponse.IsSuccessful && getResponse.Data != null)
            {
                var review = getResponse.Data.Data;
                var userId = GetUserIdFromToken();

                if (review.UserId != userId)
                {
                    ViewBag.ErrorMessage = "You can only delete your own reviews.";
                    return await Index();
                }

                var deleteRequest = new RestRequest($"Reviews/{id}", Method.Delete);
                var deleteResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(deleteRequest);

                if (!deleteResponse.IsSuccessful)
                {
                    ViewBag.ErrorMessage = deleteResponse.Data?.ErrorMessage ?? "Error deleting review.";
                    return await Index();
                }

                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Review not found.";
            return await Index();
        }
    }
}
