using System.Linq.Expressions;
using Web_AppointmentSystem.BUSINESS.DTOs.AppointmentDTOs;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.Services.Interfaces;

public interface IAppointmentService
{
    Task<bool> CreateAsync(AppointmentCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int? id, AppointmentUpdateDto dto);
    Task<AppointmentGetDto> GetByIdAsync(int id);
    Task<ICollection<AppointmentGetDto>> GetByExpressionAsync(Expression<Func<Appointment, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
    Task<ICollection<AppointmentGetDto>> GetUserAppointmentsAsync();
    Task<AppointmentGetDto> GetSingleByExpressionAsync(Expression<Func<Appointment, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
}
