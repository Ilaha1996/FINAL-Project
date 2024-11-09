namespace Web_AppointmentSystem.BUSINESS.DTOs.AppointmentDTOs;

public record AppointmentGetDto(int Id, string UserId, string UserFullname, int ServiceId, string ServiceName, DateTime Date, TimeSpan StartTime,
                                string? Notes, bool IsDeleted, DateTime CreatedDate, DateTime UpdatedDate);


