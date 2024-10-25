using Microsoft.AspNetCore.Mvc;
using Web_AppointmentSystem.MVC.ViewModels.AuthVM;

namespace Web_AppointmentSystem.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegisterVM vm);
        Task<LoginResponseVM> Login(UserLoginVM vm);
        Task<IActionResult> ConfirmEmail(ConfirmEmailVM vm);

        //Task<IActionResult> ForgotPassword(ForgotPasswordVM vm);
        //Task<IActionResult> ResetPassword(ResetPasswordVM vm);
        //Task<IActionResult> ChangePassword(ChangePasswordVM vm);

        void Logout();
    }
}
