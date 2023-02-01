using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Profile;

public class PlatformProfile : AutoMapper.Profile
{
    public PlatformProfile()
    {
        // Source --> Target 
        CreateMap<Platform, ReadPlatformDto>();
        CreateMap<CreatePlatformDto, Platform>();
    }
}