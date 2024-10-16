using System.Linq.Expressions;
using Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewGetDto> CreateAsync(ReviewCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int? id, ReviewUpdateDto dto);
    Task<ReviewGetDto> GetByIdAsync(int id);
    Task<ICollection<ReviewGetDto>> GetByExpressionAsync(Expression<Func<Review, bool>>? expression = null, bool asNoTracking = false, params string[] includes);
    Task<ReviewGetDto> GetSingleByExpressionAsync(Expression<Func<Review, bool>>? expression = null, bool asNoTracking = false, params string[] includes);

}
