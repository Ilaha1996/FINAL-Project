using Web_AppointmentSystem.BUSINESS.DTOs.ServiceImageDTOs;

namespace Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;

public record ServiceGetDto(int Id, string Name, string Description, bool IsDeleted, DateTime CreatedDate, DateTime UpdatedDate,
    decimal Price, int Duration, ServiceImageGetDto ServiceImage);
