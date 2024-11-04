using Web_AppointmentSystem.MVC.ViewModels.AuthVM;

namespace Web_AppointmentSystem.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterVM vm);
        Task<LoginResponseVM> Login(UserLoginVM vm);
        Task ConfirmEmail(string email, string token);
        void Logout();
        Task ForgotPassword(ForgotPasswordVM vm);
        Task ResetPassword(ResetPasswordVM vm);
        Task ChangePassword(ChangePasswordVM vm);
    }
}
