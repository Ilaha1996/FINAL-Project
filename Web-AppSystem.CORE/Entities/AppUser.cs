using Microsoft.AspNetCore.Identity;

namespace Web_AppointmentSystem.CORE.Entities;
public class AppUser : IdentityUser
{
    public string Fullname { get; set; }

    // Navigation properties
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<FavoriteService>? FavoriteServices { get; set; }
}
