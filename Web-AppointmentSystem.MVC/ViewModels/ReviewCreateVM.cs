namespace Web_AppointmentSystem.MVC.ViewModels
{
    public class ReviewCreateVM
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now; 
    }
}
