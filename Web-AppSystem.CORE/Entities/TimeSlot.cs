namespace Web_AppointmentSystem.CORE.Entities;

public class TimeSlot:BaseEntity
{
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }

    // Navigation properties
    public ICollection<Appointment>? Appointments { get; set; }
}
