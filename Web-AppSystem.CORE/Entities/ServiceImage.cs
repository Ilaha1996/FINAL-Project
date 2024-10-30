namespace Web_AppointmentSystem.CORE.Entities;

public class ServiceImage:BaseEntity
{
    public int ServiceID { get; set; }
    public string ImageUrl { get; set; }
    public Service Service { get; set; }
}
