namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels
{
    public record LoginVM
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public bool RememberMe { get; init; }
    }
}



