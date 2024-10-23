using Web_AppointmentSystem.MVC.ViewModels.AuthVM;

namespace Web_AppointmentSystem.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegisterVM vm);
        Task<LoginResponseVM> Login(UserLoginVM vm);
        void Logout();
    }
}
