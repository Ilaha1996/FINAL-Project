using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.BUSINESS.Utilities;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;
using static System.Net.Mime.MediaTypeNames;

namespace Web_AppointmentSystem.BUSINESS.Services.Implementations;

public class ServiceService : IServiceService
{
    private readonly IServiceRepo _serviceRepo;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    public ServiceService(IServiceRepo serviceRepo, IMapper mapper,IWebHostEnvironment env)
    {
        _serviceRepo = serviceRepo;
        _mapper = mapper;
        _env = env;
    }
    public async Task<ServiceGetDto> CreateAsync(ServiceCreateDto dto)
    {
        Service service = _mapper.Map<Service>(dto);

        string ImageUrl = dto.Image.SaveFile(_env.WebRootPath, "uploads");
        service.ServiceImage = new ServiceImage()
        {
            ImageUrl = ImageUrl,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            IsDeleted = false            
        };     

        service.CreatedDate = DateTime.Now;
        service.UpdatedDate = DateTime.Now;
        service.IsDeleted = false;

        await _serviceRepo.CreateAsync(service);
        await _serviceRepo.CommitAsync();

        ServiceGetDto getDto = _mapper.Map<ServiceGetDto>(service);

        return getDto;
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 0) throw new InvalidIdException();
        var data = await _serviceRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException();

        if (data.ServiceImage != null)
        {
            FileManager.DeleteFile(_env.WebRootPath, "uploads", data.ServiceImage.ImageUrl);
            data.ServiceImage = null;
        }

        _serviceRepo.DeleteAsync(data);
        await _serviceRepo.CommitAsync();
    }

    public async Task<ICollection<ServiceGetDto>> GetByExpressionAsync(Expression<Func<Service, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var datas = await _serviceRepo.GetByExpressionAsync(expression, asNoTracking, includes).ToListAsync();
        if (datas == null) throw new EntityNotFoundException();

        ICollection<ServiceGetDto> dtos = _mapper.Map<ICollection<ServiceGetDto>>(datas);
        return dtos;
    }

    public async Task<ServiceGetDto> GetByIdAsync(int id)
    {
        if (id < 1) throw new InvalidIdException();
        var data = await _serviceRepo.GetByIdAsync(id);
        if (data == null) throw new EntityNotFoundException();

        ServiceGetDto dto = _mapper.Map<ServiceGetDto>(data);

        return dto;
    }

    public async Task<ServiceGetDto> GetSingleByExpressionAsync(Expression<Func<Service, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var data = await _serviceRepo.GetByExpressionAsync(expression, asNoTracking, includes).FirstOrDefaultAsync();
        if (data == null) throw new EntityNotFoundException();

        ServiceGetDto dto = _mapper.Map<ServiceGetDto>(data);

        return dto;
    }

    public async Task UpdateAsync(int? id, ServiceUpdateDto dto)
    {
        if (id < 1 || id is null) throw new InvalidIdException();

        var data = await _serviceRepo.GetByIdAsync((int)id);
        if (data == null) throw new EntityNotFoundException();

        if (dto.Image != null)
        {
            if (data.ServiceImage != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "uploads", data.ServiceImage.ImageUrl);
            }

            string imageUrl = dto.Image.SaveFile(_env.WebRootPath, "uploads");
            data.ServiceImage = new ServiceImage()
            {
                ImageUrl = imageUrl,
                CreatedDate = data.ServiceImage?.CreatedDate ?? DateTime.Now, 
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };
        }

        _mapper.Map(dto, data);

        data.UpdatedDate = DateTime.Now;
        await _serviceRepo.CommitAsync();
    }
}
