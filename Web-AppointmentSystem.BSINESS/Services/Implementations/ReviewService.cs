using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Web.AppointmentSystem.DATA.Repostories;
using Web_AppointmentSystem.BUSINESS.DTOs.AppointmentDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web_AppointmentSystem.BUSINESS.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepo reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }
        public async Task<ReviewGetDto> CreateAsync(ReviewCreateDto dto)
        {
            Review review = _mapper.Map<Review>(dto);
            review.CreatedDate = DateTime.Now;
            review.UpdatedDate = DateTime.Now;
            review.IsDeleted = false;

            await _reviewRepo.CreateAsync(review);
            await _reviewRepo.CommitAsync();

            ReviewGetDto getDto = _mapper.Map<ReviewGetDto>(review);

            return getDto;
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 0) throw new InvalidIdException();
            var data = await _reviewRepo.GetByIdAsync(id);
            if (data == null) throw new EntityNotFoundException();

            _reviewRepo.DeleteAsync(data);
            await _reviewRepo.CommitAsync();
        }

        public async Task<ICollection<ReviewGetDto>> GetByExpressionAsync(Expression<Func<Review, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
        {
            var datas = await _reviewRepo.GetByExpressionAsync(expression, asNoTracking, includes).ToListAsync();
            if (datas == null) throw new EntityNotFoundException();

            ICollection<ReviewGetDto> dtos = _mapper.Map<ICollection<ReviewGetDto>>(datas);
            return dtos;
        }

        public async Task<ReviewGetDto> GetByIdAsync(int id)
        {
            if (id < 1) throw new InvalidIdException();
            var data = await _reviewRepo.GetByIdAsync(id);
            if (data == null) throw new EntityNotFoundException();

            ReviewGetDto dto = _mapper.Map<ReviewGetDto>(data);

            return dto;
        }

        public async Task<ReviewGetDto> GetSingleByExpressionAsync(Expression<Func<Review, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
        {
            var data = await _reviewRepo.GetByExpressionAsync(expression, asNoTracking, includes).FirstOrDefaultAsync();
            if (data == null) throw new EntityNotFoundException();

            ReviewGetDto dto = _mapper.Map<ReviewGetDto>(data);

            return dto;
        }

        public async Task UpdateAsync(int? id, ReviewUpdateDto dto)
        {
            if (id < 1 || id is null) throw new InvalidIdException();

            var data = await _reviewRepo.GetByIdAsync((int)id);
            if (data == null) throw new EntityNotFoundException();

            _mapper.Map(dto, data);

            data.UpdatedDate = DateTime.Now;
            await _reviewRepo.CommitAsync();
        }
    }
}
