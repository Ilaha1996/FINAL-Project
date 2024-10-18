namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.AppointmentVM;

public record AppointmentGetVM(int Id, string UserId, string UserFullname, int ServiceId, int TimeSlotId, bool IsConfirmed,
                            string? Notes, bool IsDeleted, DateTime CreatedDate, DateTime UpdatedDate);
