namespace Web_AppointmentSystem.BUSINESS.DTOs.AppointmentDTOs;

public record AppointmentGetDto(int Id, string UserId, string UserFullname, int ServiceId, int TimeSlotId, bool IsConfirmed,
                                string? Notes, bool IsDeleted, DateTime CreatedDate, DateTime UpdatedDate);


