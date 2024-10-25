using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceVM;
using Web_AppointmentSystem.MVC.Services.Implementations;

namespace Web_AppointmentSystem.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ServiceFilter(typeof(TokenFilter))]
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
                // Check if response.Data is null before accessing ErrorMessage
                if (response.Data != null)
                {
                    ViewBag.Err = response.Data.ErrorMessage;
                }
                else
                {
                    ViewBag.Err = "An error occurred, but no further details are available.";
                }

                return View();
            }

            // Also check if response.Data is null before trying to pass it to the view
            if (response.Data?.Data == null)
            {
                ViewBag.Err = "No data received from the server.";
                return View();
            }

            return View(response.Data.Data);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var serviceRequest = new RestRequest("services", Method.Post);
            serviceRequest.AddJsonBody(vm);

            var serviceResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(serviceRequest);

            if (serviceResponse == null || !serviceResponse.IsSuccessful)
            {
                var errorMessage = serviceResponse?.Data?.ErrorMessage ?? "An unexpected error occurred.";
                ModelState.AddModelError("", errorMessage);
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var serviceRequest = new RestRequest($"services/{id}", Method.Get);
            var serviceResponse = await _restClient.ExecuteAsync<ApiResponseMessage<ServiceGetVM>>(serviceRequest);

            if (serviceResponse == null || !serviceResponse.IsSuccessful || serviceResponse.Data == null || serviceResponse.Data.Data == null)
            {
                string errorMessage = serviceResponse?.Data?.ErrorMessage ?? "An unexpected error occurred.";
                ViewBag.Err = errorMessage;
                return View();
            }

            ServiceUpdateVM vm = new ServiceUpdateVM(
                serviceResponse.Data.Data.Name,
                serviceResponse.Data.Data.Description,
                serviceResponse.Data.Data.IsDeleted,
                serviceResponse.Data.Data.Price,
                serviceResponse.Data.Data.Duration                
             );

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ServiceUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var serviceRequest = new RestRequest($"services/{id}", Method.Put);

            serviceRequest.AddJsonBody(new
            {
                Name = vm.Name,
                Description = vm.Description,
                IsDeleted = vm.IsDeleted,
                Price = vm.Price,
                Duration = vm.Duration,
            });

            var serviceResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(serviceRequest);

            if (!serviceResponse.IsSuccessful)
            {
                string errorMessage = serviceResponse?.Data?.ErrorMessage ?? "An unexpected error occurred.";
                ModelState.AddModelError("", errorMessage);
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var request = new RestRequest($"services/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
