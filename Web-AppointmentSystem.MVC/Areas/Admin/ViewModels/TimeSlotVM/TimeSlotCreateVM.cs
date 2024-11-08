namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.TimeSlotVM;

public class TimeSlotCreateVM
{
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsDeleted {  get; set; } 

}



