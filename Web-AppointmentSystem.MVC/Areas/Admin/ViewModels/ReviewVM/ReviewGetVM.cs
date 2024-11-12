namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ReviewVM;

public record ReviewGetVM(int Id, string Comment, int Rating, string UserId, string UserFullname,DateTime CreatedDate);

