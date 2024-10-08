using AutoMapper;
using Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;
using Web_AppointmentSystem.CORE.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web_AppointmentSystem.BUSINESS.MappingProfiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<ServiceGetDto, Service>().ReverseMap();
        CreateMap<ServiceCreateDto, Service>().ReverseMap();
        CreateMap<ServiceUpdateDto, Service>().ReverseMap();



       
    }
}