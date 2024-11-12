namespace Web_AppointmentSystem.CORE.Entities;

public class Review:BaseEntity
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public string UserId { get; set; } 
    public AppUser User { get; set; } 
}

