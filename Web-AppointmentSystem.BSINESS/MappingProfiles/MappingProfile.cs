using AutoMapper;
using Web_AppointmentSystem.BUSINESS.DTOs.AppointmentDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.ReviewDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.ServiceDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.ServiceImageDTOs;
using Web_AppointmentSystem.BUSINESS.DTOs.TimeSlotDTOs;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.BUSINESS.MappingProfiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<ServiceGetDto, Service>().ReverseMap();
        CreateMap<ServiceCreateDto, Service>().ReverseMap();
        CreateMap<ServiceUpdateDto, Service>().ReverseMap();

        CreateMap<AppointmentGetDto, Appointment>().ReverseMap();
        CreateMap<AppointmentCreateDto, Appointment>().ReverseMap();
        CreateMap<AppointmentUpdateDto, Appointment>().ReverseMap();

        CreateMap<ReviewGetDto, Review>().ReverseMap();
        CreateMap<ReviewCreateDto, Review>().ReverseMap();
        CreateMap<ReviewUpdateDto, Review>().ReverseMap();

        CreateMap<TimeSlotGetDto, TimeSlot>().ReverseMap();
        CreateMap<TimeSlotCreateDto, TimeSlot>().ReverseMap();
        CreateMap<TimeSlotUpdateDto, TimeSlot>().ReverseMap();

        CreateMap<ServiceImage, ServiceImageGetDto>().ReverseMap();

    }
}