using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ReviewVM;
using Web_AppointmentSystem.MVC.Services.Implementations;

namespace Web_AppointmentSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(TokenFilter))]
    public class ReviewController : Controller
    {
        private readonly RestClient _restClient;
        public ReviewController()
        {
            _restClient = new RestClient("https://localhost:7197/api");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("reviews", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<ReviewGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return View(response.Data.Data);
        }
    }
}
