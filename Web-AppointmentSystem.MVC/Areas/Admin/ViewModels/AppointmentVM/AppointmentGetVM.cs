namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.AppointmentVM;

public record AppointmentGetVM(int Id, string UserId, string UserFullname, int ServiceId, string ServiceName, DateTime Date, TimeSpan StartTime, 
                            string? Notes, bool IsDeleted, DateTime CreatedDate, DateTime UpdatedDate);
