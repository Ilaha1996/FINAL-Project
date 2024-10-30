using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceImageVM;

namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceVM;

public record ServiceCreateVM(string Name, string Description, bool IsDeleted, decimal Price, int Duration, IFormFile Image);

