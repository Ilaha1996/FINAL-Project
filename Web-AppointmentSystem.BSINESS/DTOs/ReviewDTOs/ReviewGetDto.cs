namespace Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;

public record ReviewGetDto(int Id, string? Comment, int Rating, int AppointmentId);

