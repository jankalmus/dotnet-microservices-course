using CommandService.DTOs;
using CommandService.Model;

namespace CommandService.Profiles;

public class PlatformProfile : AutoMapper.Profile
{
    public PlatformProfile()
    {
        // Source --> Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(dest => dest.ExternalId, 
                opt => opt.MapFrom(src => src.Id));
    }
}