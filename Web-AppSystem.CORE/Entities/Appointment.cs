namespace Web_AppointmentSystem.CORE.Entities;

public class Appointment : BaseEntity
{
    public string UserId { get; set; }
    public int ServiceId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public AppUser User { get; set; }
    public Service Service { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}
