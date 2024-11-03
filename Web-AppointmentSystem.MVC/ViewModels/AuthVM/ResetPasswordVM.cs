namespace Web_AppointmentSystem.MVC.ViewModels.AuthVM;

public record ResetPasswordVM(string Email, string Token, string Password, string ConfirmPassword);

