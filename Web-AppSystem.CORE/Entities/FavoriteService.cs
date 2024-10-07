namespace Web_AppointmentSystem.CORE.Entities;

public class FavoriteService
{
    public string UserId { get; set; }
    public int ServiceId { get; set; }

    // Navigation properties
    public AppUser User { get; set; }
    public Service Service { get; set; }
}

