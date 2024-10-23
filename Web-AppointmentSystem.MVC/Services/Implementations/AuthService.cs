using RestSharp;
using Web_AppointmentSystem.MVC.APIResponseMessages;
using Web_AppointmentSystem.MVC.Services.Interfaces;
using Web_AppointmentSystem.MVC.ViewModels.AuthVM;

namespace Web_AppointmentSystem.MVC.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly RestClient _restClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _restClient = new RestClient(_configuration.GetSection("API:Base_Url").Value);
    }
    public async Task<LoginResponseVM> Login(UserLoginVM vm)
    {
        var request = new RestRequest("/auth/login", Method.Post);
        request.AddJsonBody(vm);

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<LoginResponseVM>>(request);
        if (!response.IsSuccessful) throw new Exception();

        return response.Data.Data;
    }

    public void Logout()
    {
       _httpContextAccessor.HttpContext.Response.Cookies.Delete("token");
    }     

    public async Task Register(UserRegisterVM vm)
    {
        var request = new RestRequest("/auth/register", Method.Post);
        request.AddJsonBody(vm);

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);
        if (!response.IsSuccessful || response.Data == null)
        {
            throw new Exception(response?.ErrorMessage ?? "Registration failed");
        }

    }
}
