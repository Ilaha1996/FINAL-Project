using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Web.AppointmentSystem.DATA.Repostories;
using Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;
using Web_AppointmentSystem.BUSINESS.Exceptions.CommonExceptions;
using Web_AppointmentSystem.BUSINESS.Services.Interfaces;
using Web_AppointmentSystem.BUSINESS.Utilities;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.CORE.Repostories;

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
        service.ServiceImages = new List<ServiceImage>();
        ServiceImage serviceImage = new ServiceImage()
        {
            ImageUrl = ImageUrl,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            IsDeleted = false            
        };     
        service.ServiceImages.Add(serviceImage);

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
        var data = await _serviceRepo.GetByExpressionAsync(x=>x.Id ==id, false,"ServiceImages").FirstOrDefaultAsync();
        if (data == null) throw new EntityNotFoundException(404, "Not Found");

        ServiceImage existedImage = data.ServiceImages.FirstOrDefault();
        if (existedImage != null)
        {
            FileManager.DeleteFile(_env.WebRootPath, "uploads", existedImage.ImageUrl);
            data.ServiceImages.Remove(existedImage);
        }

        _serviceRepo.DeleteAsync(data);
        await _serviceRepo.CommitAsync();
    }

    public async Task<ICollection<ServiceGetDto>> GetByExpressionAsync(Expression<Func<Service, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        IQueryable<Service> query = _serviceRepo.GetByExpressionAsync(expression, asNoTracking);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        var datas = await _serviceRepo.GetByExpressionAsync(expression, asNoTracking, includes).ToListAsync();
        if (datas == null) throw new EntityNotFoundException(404, "Not Found");

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
        if (data == null) throw new EntityNotFoundException(404, "Not Found");

        ServiceGetDto dto = _mapper.Map<ServiceGetDto>(data);

        return dto;
    }

    public async Task UpdateAsync(int? id, ServiceUpdateDto dto)
    {
        if (id < 1 || id is null) throw new InvalidIdException();

        var data = await _serviceRepo.GetByIdAsync((int)id);
        if (data == null) throw new EntityNotFoundException(404, "Not Found");

        string ImageUrl = dto.Image.SaveFile(_env.WebRootPath, "uploads");
        if (data.ServiceImages != null)
        {
            ServiceImage existedImage = data.ServiceImages.FirstOrDefault();
            if (existedImage != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "uploads", existedImage.ImageUrl);
                data.ServiceImages.Remove(existedImage);
            }

            ServiceImage newImage = new ServiceImage
            {
                ImageUrl = ImageUrl,
                UpdatedDate = DateTime.Now,
                IsDeleted = false
            };
            data.ServiceImages.Add(newImage);
        }

        _mapper.Map(dto, data);

        data.UpdatedDate = DateTime.Now;
        await _serviceRepo.CommitAsync();
    }
}
