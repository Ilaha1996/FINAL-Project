using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Web.AppointmentSystem.DATA.Repostories;
using Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.TimeSlotDTOs;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web_AppointmentSystem.BUSINESS.Services.Implementations;

public class TimeSlotService : ITimeSlotService
{
    private readonly ITimeSlotRepo _timeSlotRepo;
    private readonly IMapper _mapper;
    public TimeSlotService(ITimeSlotRepo timeSlotRepo, IMapper mapper)
    {
        _timeSlotRepo = timeSlotRepo;
        _mapper = mapper;
    }
    public async Task CreateAsync(TimeSlotCreateDto dto)
    {
        TimeSlot timeSlot = _mapper.Map<TimeSlot>(dto);
        timeSlot.CreatedDate = DateTime.Now;
        timeSlot.UpdatedDate = DateTime.Now;
        timeSlot.IsDeleted = false;

        await _timeSlotRepo.CreateAsync(timeSlot);
        await _timeSlotRepo.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id > 0) throw new InvalidIdException();
        var data = await _timeSlotRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException();

        _timeSlotRepo.DeleteAsync(data);
        await _timeSlotRepo.CommitAsync();
    }

    public async Task<ICollection<TimeSlotGetDto>> GetByExpressionAsync(Expression<Func<TimeSlot, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var datas = await _timeSlotRepo.GetByExpressionAsync(expression, asNoTracking, includes).ToListAsync();
        if (datas == null) throw new EntityNotFoundException();

        ICollection<TimeSlotGetDto> dtos = _mapper.Map<ICollection<TimeSlotGetDto>>(datas);
        return dtos;
    }

    public async Task<TimeSlotGetDto> GetByIdAsync(int id)
    {
        if (id < 1) throw new InvalidIdException();
        var data = await _timeSlotRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException();

        TimeSlotGetDto dto = _mapper.Map<TimeSlotGetDto>(data);

        return dto;
    }

    public async Task<TimeSlotGetDto> GetSingleByExpressionAsync(Expression<Func<TimeSlot, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var data = await _timeSlotRepo.GetByExpressionAsync(expression, asNoTracking, includes).FirstOrDefaultAsync();
        if (data == null) throw new EntityNotFoundException();

        TimeSlotGetDto dto = _mapper.Map<TimeSlotGetDto>(data);

        return dto;
    }

    public async Task UpdateAsync(int? id, TimeSlotUpdateDto dto)
    {
        if (id < 1 || id is null) throw new InvalidIdException();

        var data = await _timeSlotRepo.GetByIdAsync((int)id);
        if (data == null) throw new EntityNotFoundException();

        _mapper.Map(dto, data);

        data.UpdatedDate = DateTime.Now;
        await _timeSlotRepo.CommitAsync();
    }
}
