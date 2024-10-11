using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.DTOs.TimeSlotDTOs;

public record TimeSlotGetDto(int Id, DateTime Date, TimeSpan StartTime, TimeSpan EndTime, bool IsAvailable, ICollection<Appointment>? Appointments);

