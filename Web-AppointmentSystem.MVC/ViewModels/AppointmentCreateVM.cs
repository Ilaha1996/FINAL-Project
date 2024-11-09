using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceVM;

namespace Web_AppointmentSystem.MVC.ViewModels;

public class AppointmentCreateVM
{
    public int ServiceId { get; set; }
    public string UserId { get; set; }
    public string? Notes { get; set; }

    public DateTime Date { get; set; } 
    public TimeSpan StartTime { get; set; } 

    public List<ServiceGetVM> Services { get; set; } 
    public List<(DateTime Date, TimeSpan StartTime)> AvailableTimeSlots { get; set; } 
}
