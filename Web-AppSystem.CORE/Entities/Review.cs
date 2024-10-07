namespace Web_AppointmentSystem.CORE.Entities;

public class Review:BaseEntity
{
    public int AppointmentId { get; set; }  
    public int Rating { get; set; }  
    public string Comment { get; set; }

    // Navigation properties
    public Appointment Appointment { get; set; }
}

