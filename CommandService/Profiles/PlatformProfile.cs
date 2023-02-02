using CommandService.DTOs;
using CommandService.Model;
using PlatformService;

namespace CommandService.Profiles;

public class PlatformProfile : AutoMapper.Profile
{
    public PlatformProfile()
    {
        // Source --> Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
        CreateMap<GrpcPlatformModel, Platform>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Commands, opt => opt.Ignore()); 
    }
}