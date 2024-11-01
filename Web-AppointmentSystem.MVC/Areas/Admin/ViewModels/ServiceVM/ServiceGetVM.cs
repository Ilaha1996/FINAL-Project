using Web_AppointmentSystem.BUSINESS.DTOs.ServiceImageDTOs;
using Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceImageVM;

namespace Web_AppointmentSystem.MVC.Areas.Admin.ViewModels.ServiceVM;

public record ServiceGetVM(int Id, string Name, string Description, bool IsDeleted, DateTime CreatedDate, DateTime UpdatedDate,
                            decimal Price, int Duration, ICollection<ServiceImageGetVM> ServiceImages);

