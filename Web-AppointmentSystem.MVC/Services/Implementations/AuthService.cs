using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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

    public async Task ConfirmEmail(string email, string token)
    {
        var request = new RestRequest("/auth/confirmEmail", Method.Post);
        request.AddJsonBody(new { email, token });  

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);        
    }
    public async Task<LoginResponseVM> Login(UserLoginVM vm)
    {
        var request = new RestRequest("/auth/login", Method.Post);
        request.AddJsonBody(vm);

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<LoginResponseVM>>(request);
        if (!response.IsSuccessful || response.Data == null)
        {
            throw new Exception(response?.ErrorMessage ?? "Login failed");
        }

        return response.Data.Data;
    }
    public void Logout()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("token");
    }
    public async Task<string> Register(UserRegisterVM vm)
    {
        var request = new RestRequest("/auth/register", Method.Post);
        request.AddJsonBody(vm);

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);
        if (!response.IsSuccessful || response.Data == null)
        {
            throw new Exception(response?.ErrorMessage ?? "Registration failed");
        }

        return response.Data.Data?.ToString() ?? throw new Exception("Unexpected response format");
    }
    public async Task ResetPassword(ResetPasswordVM vm)
    {
        var request = new RestRequest("/auth/resetPassword", Method.Post);
        request.AddJsonBody(vm);

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

        if (!response.IsSuccessful || response.Data == null)
        {
            var errorMessage = response?.ErrorMessage ?? "Password reset failed. Please try again.";
            throw new Exception(errorMessage);
        }

        if (response.Data.Data == null)
        {
            throw new Exception("Unexpected response format: Data is missing.");
        }   
    }
    public async Task ChangePassword(ChangePasswordVM vm)
    {
        var request = new RestRequest("/auth/changePassword", Method.Post);
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["token"];
        vm.Token = token;
        request.AddJsonBody(vm);

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

        if (!response.IsSuccessful || response.Data == null)
        {
            var errorMessage = response?.ErrorMessage ?? "Password change failed. Please try again.";
            throw new Exception(errorMessage);
        }

        if (response.Data.Data == null)
        {
            throw new Exception("Unexpected response format: Data is missing.");
        }
    }
    public async Task ForgotPassword(ForgotPasswordVM vm)
    {
        var request = new RestRequest("/auth/forgotPassword", Method.Post);
        request.AddJsonBody(vm);

        var response = await _restClient.ExecuteAsync<ApiResponseMessage<object>>(request);

        if (!response.IsSuccessful || response.Data == null)
        {
            var errorMessage = response?.ErrorMessage ?? "Password reset request failed. Please try again.";
            throw new Exception(errorMessage);
        }

        if (response.Data.Data == null)
        {
            throw new Exception("Unexpected response format: Data is missing.");
        }

    }

}
