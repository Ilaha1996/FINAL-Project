namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceVM;

public record ServiceUpdateVM(string Name, string Description, bool IsDeleted, decimal Price, int Duration, IFormFile? Image);

