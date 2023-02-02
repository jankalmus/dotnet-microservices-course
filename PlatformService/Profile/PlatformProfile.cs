using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Profile;

public class PlatformProfile : AutoMapper.Profile
{
    public PlatformProfile()
    {
        // Source --> Target 
        CreateMap<Platform, ReadPlatformDto>();
        CreateMap<CreatePlatformDto, Platform>();
        CreateMap<ReadPlatformDto, PlatformPublishedDto>();
        CreateMap<Platform, GrpcPlatformModel>()
            .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher)); 
    }
}