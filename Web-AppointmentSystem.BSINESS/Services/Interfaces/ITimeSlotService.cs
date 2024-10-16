using System.Linq.Expressions;
using Web_AppointmentSystem.BUSINESS.DTOs.TimeSlotDTOs;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.Services.Interfaces
{
    public interface ITimeSlotService
    {
        Task<TimeSlotGetDto> CreateAsync(TimeSlotCreateDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int? id, TimeSlotUpdateDto dto);
        Task<TimeSlotGetDto> GetByIdAsync(int id);
        Task<ICollection<TimeSlotGetDto>> GetByExpressionAsync(Expression<Func<TimeSlot, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
        Task<TimeSlotGetDto> GetSingleByExpressionAsync(Expression<Func<TimeSlot, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
    }
}

