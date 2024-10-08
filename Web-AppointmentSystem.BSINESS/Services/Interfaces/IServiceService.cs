using System.Linq.Expressions;
using Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.Services.Interfaces;

public interface IServiceService
{
    Task CreateAsync(ServiceCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int? id, ServiceUpdateDto dto);
    Task<ServiceGetDto> GetByIdAsync(int id);
    Task<ICollection<ServiceGetDto>> GetByExpressionAsync(Expression<Func<Service, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
    Task<ServiceGetDto> GetSingleByExpressionAsync(Expression<Func<Service, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
}
