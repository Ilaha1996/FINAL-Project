﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using Web_AppointmentSystem.BUSINESS.DTOs.AppointmentDTOs;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

namespace Web_AppointmentSystem.BUSINESS.Services.Implementations;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepo _appointmentRepo;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppointmentService(IAppointmentRepo appointmentRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _appointmentRepo = appointmentRepo;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<bool> CreateAsync(AppointmentCreateDto dto)
    {
        bool isAvailable = await _appointmentRepo.IsDateTimeAvailableForServiceAsync(dto.ServiceId, dto.Date, dto.StartTime);

        if (!isAvailable)
        {
            throw new Exception("The selected date and time are already booked for this service.");
        }

        Appointment appointment = _mapper.Map<Appointment>(dto);
        appointment.CreatedDate = DateTime.Now;
        appointment.UpdatedDate = DateTime.Now;
        appointment.IsDeleted = false;

        await _appointmentRepo.CreateAsync(appointment);
        await _appointmentRepo.CommitAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0) throw new InvalidIdException();
        var data = await _appointmentRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException();

        _appointmentRepo.DeleteAsync(data);
        await _appointmentRepo.CommitAsync();
    }

    public async Task<ICollection<AppointmentGetDto>> GetByExpressionAsync(Expression<Func<Appointment, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        IQueryable<Appointment> query = _appointmentRepo.GetByExpressionAsync(expression, asNoTracking);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        var datas = await _appointmentRepo.GetByExpressionAsync(expression, asNoTracking, includes).ToListAsync();
        if (datas == null) throw new EntityNotFoundException();

        ICollection<AppointmentGetDto> dtos = _mapper.Map<ICollection<AppointmentGetDto>>(datas);
        return dtos;
    }

    public async Task<ICollection<AppointmentGetDto>> GetUserAppointmentsAsync(Expression<Func<Appointment, bool>>? expression, bool asNoTracking = false, params string[] includes)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        IQueryable<Appointment> query = _appointmentRepo.GetByExpressionAsync(expression, asNoTracking);

        if (includes != null && includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        query = query.Where(a => a.UserId == userId);

        var appointments = await query.ToListAsync();

        if (appointments.Count == 0) throw new EntityNotFoundException("No appointments found for the user.");

        return _mapper.Map<ICollection<AppointmentGetDto>>(appointments);
    }

    public async Task<AppointmentGetDto> GetByIdAsync(int id)
    {
        if (id < 1) throw new InvalidIdException();
        var data = await _appointmentRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException();

        AppointmentGetDto dto = _mapper.Map<AppointmentGetDto>(data);

        return dto;
    }

    public async Task<AppointmentGetDto> GetSingleByExpressionAsync(Expression<Func<Appointment, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var data = await _appointmentRepo.GetByExpressionAsync(expression, asNoTracking, includes).FirstOrDefaultAsync();
        if (data == null) throw new EntityNotFoundException();

        AppointmentGetDto dto = _mapper.Map<AppointmentGetDto>(data);

        return dto;
    }

    public async Task UpdateAsync(int? id, AppointmentUpdateDto dto)
    {
        if (id < 1 || id is null) throw new InvalidIdException();

        var data = await _appointmentRepo.GetByIdAsync((int)id);
        if (data == null) throw new EntityNotFoundException();

        _mapper.Map(dto, data);

        data.UpdatedDate = DateTime.Now;
        await _appointmentRepo.CommitAsync();
    }
}
