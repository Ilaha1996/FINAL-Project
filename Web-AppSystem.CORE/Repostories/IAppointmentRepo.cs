using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.CORE.Repostories;
public interface IAppointmentRepo : IGenericRepo<Appointment> 
{
    public Task<bool> IsDateTimeAvailableForServiceAsync(int serviceId, DateTime date, TimeSpan startTime);
}
