using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.AppointmentVM;

namespace Web_AppointmentSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentController : Controller
    {
        private readonly RestClient _restClient;
        public AppointmentController()
        {
            _restClient = new RestClient("https://localhost:7197/api");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("appointments", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<AppointmentGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return View(response.Data.Data);
        }
    }
}
