using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceVM;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.TimeSlotVM;
using Web_AppointmentSystem.MVC.ViewModels;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly RestClient _restClient;

        public AppointmentController()
        {
            _restClient = new RestClient("https://localhost:7197/api");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serviceRequest = new RestRequest("services", Method.Get);
            var serviceResponse = await _restClient.ExecuteAsync<ApiResponseMessage<List<ServiceGetVM>>>(serviceRequest);

            if (!serviceResponse.IsSuccessful || serviceResponse.Data?.Data == null)
            {
                ViewBag.Err = serviceResponse.Data?.ErrorMessage ?? "An error occurred while retrieving services.";
                return View();
            }

            var services = serviceResponse.Data.Data;

            var token = HttpContext.Request.Headers["token"].ToString();

            string userId = null;
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }

            var availableTimeSlots = GenerateAvailableTimeSlots();

            var model = new AppointmentCreateVM
            {
                UserId = userId,
                Services = services,
                AvailableTimeSlots = availableTimeSlots
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(AppointmentCreateVM model)
        {
            //if (!ModelState.IsValid)
            //{
            //    var serviceRequest = new RestRequest("services", Method.Get);
            //    var serviceResponse = await _restClient.ExecuteAsync<ApiResponseMessage<List<ServiceGetVM>>>(serviceRequest);

            //    if (!serviceResponse.IsSuccessful || serviceResponse.Data?.Data == null)
            //    {
            //        ViewBag.Err = serviceResponse.Data?.ErrorMessage ?? "An error occurred while retrieving services.";
            //        return View(model);
            //    }

            //    model.Services = serviceResponse.Data.Data;
            //    model.AvailableTimeSlots = GenerateAvailableTimeSlots();
            //    return View(model);
            //}

            var appointmentRequest = new RestRequest("appointments", Method.Post);
            appointmentRequest.AddJsonBody(new
            {
                ServiceId = model.ServiceId,
                UserId = model.UserId,
                Date = model.Date,
                StartTime = model.StartTime
            });

            var appointmentResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(appointmentRequest);

            if (!appointmentResponse.IsSuccessful)
            {
                ViewBag.Err = appointmentResponse.Data?.ErrorMessage ?? "An error occurred while creating the appointment.";

                var serviceRequest = new RestRequest("services", Method.Get);
                var serviceResponse = await _restClient.ExecuteAsync<ApiResponseMessage<List<ServiceGetVM>>>(serviceRequest);
                model.Services = serviceResponse.Data?.Data;
                model.AvailableTimeSlots = GenerateAvailableTimeSlots();

                return View(model);
            }

            return RedirectToAction("Index", "Appointment");
        }    

        private List<TimeSlotCreateVM> GenerateAvailableTimeSlots()
        {
            var availableTimeSlots = new List<TimeSlotCreateVM>();
            var startTime = new TimeSpan(9, 0, 0);
            var endTime = new TimeSpan(18, 0, 0);
            var totalDays = 30;

            for (var day = 1; day <= totalDays; day++)
            {
                var date = DateTime.Now.Date.AddDays(day);

                for (var time = startTime; time < endTime; time = time.Add(new TimeSpan(1, 0, 0)))
                {
                    availableTimeSlots.Add(new TimeSlotCreateVM
                    {
                        Date = date,
                        StartTime = time,
                        IsAvailable = true,
                        IsDeleted = false
                    });
                }
            }

            return availableTimeSlots;
        }

    }

}
