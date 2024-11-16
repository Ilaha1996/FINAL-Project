namespace Web_AppointmentSystem.MVC.ViewModels
{
    public class ReviewCreateVM
    {
        public string? Comment { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedDate { get; set; } 

        public bool IsDeleted = false;
    }
}
