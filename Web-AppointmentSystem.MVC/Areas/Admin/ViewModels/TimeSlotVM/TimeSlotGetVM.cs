using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.AppointmentVM;

namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.TimeSlotVM;
public record TimeSlotGetVM(int Id, DateTime Date, TimeSpan StartTime, TimeSpan EndTime, bool IsAvailable, ICollection<AppointmentGetVM>? Appointments);
