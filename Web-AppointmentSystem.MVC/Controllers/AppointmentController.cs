using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.AppointmentVM;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceVM;
using Web_AppointmentSystem.MVC.ViewModels;

namespace Web_AppointmentSystem.MVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly RestClient _restClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentController(IHttpContextAccessor httpContextAccessor)
        {
            _restClient = new RestClient("https://localhost:7197/api");
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serviceRequest = new RestRequest("services", Method.Get);
            var serviceResponse = await _restClient.ExecuteAsync<ApiResponseMessage<List<ServiceGetVM>>>(serviceRequest);

            List<ServiceGetVM> services = serviceResponse.IsSuccessful && serviceResponse.Data?.Data != null
                ? serviceResponse.Data.Data
                : new List<ServiceGetVM>();

            var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            string userId = null;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);
                    userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Token parsing error: " + ex.Message);
                }
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
            var serviceRequest = new RestRequest("services", Method.Get);
            var serviceResponse = await _restClient.ExecuteAsync<ApiResponseMessage<List<ServiceGetVM>>>(serviceRequest);

            model.Services = serviceResponse.IsSuccessful && serviceResponse.Data?.Data != null
                ? serviceResponse.Data.Data
                : new List<ServiceGetVM>();

            var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);
                    model.UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Token parsing error: " + ex.Message);
                }
            }

            model.AvailableTimeSlots = GenerateAvailableTimeSlots();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var appointmentRequest = new RestRequest("appointments", Method.Post);
            appointmentRequest.AddParameter("ServiceId", model.ServiceId);
            appointmentRequest.AddParameter("UserId", model.UserId);
            appointmentRequest.AddParameter("Notes", model.Notes);
            appointmentRequest.AddParameter("Date", model.Date.ToString("yyyy-MM-dd")); 
            appointmentRequest.AddParameter("StartTime", model.StartTime.ToString(@"hh\:mm")); 

            var appointmentResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(appointmentRequest);

            if (!appointmentResponse.IsSuccessful)
            {
                var errorMessage = appointmentResponse?.Data?.ErrorMessage ?? "An error occurred while creating the appointment.";
                ModelState.AddModelError("", errorMessage);
                return View(model);
            }

            return RedirectToAction("Index", "MyAppointment");
        }

        private List<(DateTime Date, TimeSpan StartTime)> GenerateAvailableTimeSlots()
        {
            var availableTimeSlots = new List<(DateTime Date, TimeSpan StartTime)>();
            var startTime = new TimeSpan(9, 0, 0);
            var endTime = new TimeSpan(18, 0, 0);
            var totalDays = 30;

            for (var day = 1; day <= totalDays; day++)
            {
                var date = DateTime.Now.Date.AddDays(day);

                for (var time = startTime; time < endTime; time = time.Add(new TimeSpan(1, 0, 0)))
                {
                    availableTimeSlots.Add((Date: date, StartTime: time));
                }
            }

            return availableTimeSlots;
        }

        public async Task<IActionResult> MyAppointment()
        {
            var token = HttpContext.Request.Cookies["token"];
            var request = new RestRequest("appointments/userAppointments", Method.Get);
            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<AppointmentGetVM>>>(request);

            if (!response.IsSuccessful || response.Data?.Data == null)
            {
                ViewBag.Err = response.Data?.ErrorMessage ?? "Error retrieving appointments.";
                return View(new List<AppointmentGetVM>()); 
            }

            return View(response.Data.Data);
        }
        public async Task<IActionResult> CancelAppointment(int id, DateTime appointmentDate)
        {
            if (appointmentDate < DateTime.Now.Date)
            {
                TempData["Err"] = "You cannot cancel an appointment that is in the past.";
                return RedirectToAction("MyAppointment");
            }

            if (appointmentDate.Date == DateTime.Now.Date)
            {
                TempData["Err"] = "You cannot cancel an appointment on the day of the appointment.";
                return RedirectToAction("MyAppointment");
            }

            if ((appointmentDate - DateTime.Now).TotalDays < 1)
            {
                TempData["Err"] = "Appointments can only be cancelled at least one day in advance.";
                return RedirectToAction("MyAppointment");
            }

            var deleteRequest = new RestRequest($"appointments/{id}", Method.Delete);
            var deleteResponse = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(deleteRequest);

            if (!deleteResponse.IsSuccessful)
            {
                TempData["Err"] = deleteResponse.Data?.ErrorMessage ?? "Error cancelling appointment.";
                return RedirectToAction("MyAppointment");
            }

            return RedirectToAction("MyAppointment");
        }

    }
}
