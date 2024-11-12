using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceVM;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class ServiceController : Controller
    {
        private readonly RestClient _restClient;

        public ServiceController()
        {
            _restClient = new RestClient("https://localhost:7197/api");
        }

        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("services", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<ServiceGetVM>>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data?.ErrorMessage ?? "An error occurred, but no further details are available.";
                return View();
            }

            if (response.Data?.Data == null)
            {
                ViewBag.Err = "No data received from the server.";
                return View();
            }

            var services = response.Data.Data;
            var midIndex = services.Count / 2;

            var model = new ServicePageVM
            {
                LeftServices = services.Take(midIndex).ToList(),
                RightServices = services.Skip(midIndex).ToList()
            };

            return View(model);
        }
    }
}
