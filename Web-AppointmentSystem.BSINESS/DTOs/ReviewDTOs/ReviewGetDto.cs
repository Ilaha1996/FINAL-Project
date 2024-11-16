namespace Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;

public record ReviewGetDto(int Id, string Comment, string UserId, string UserFullname,DateTime CreatedDate);

