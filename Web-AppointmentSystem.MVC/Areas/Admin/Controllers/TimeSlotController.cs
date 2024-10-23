using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.AppointmentVM;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.TimeSlotVM;

namespace Web_AppointmentSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TimeSlotController : Controller
    {
        private readonly RestClient _restClient;
        public TimeSlotController()
        {
            _restClient = new RestClient("https://localhost:7197/api");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("timeSlots", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<TimeSlotGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return View(response.Data.Data);
        }
    }
}
