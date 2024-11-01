namespace Web_AppointmentSystem.CORE.Entities;

public class Service:BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }

    // Navigation properties
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<ServiceImage> ServiceImages { get; set; }
}
