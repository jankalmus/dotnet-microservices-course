using CommandService.DTOs;
using CommandService.Model;

namespace CommandService.Profiles;

public class PlatformProfile : AutoMapper.Profile
{
    public PlatformProfile()
    {
        // Source --> Target
        CreateMap<Platform, PlatformReadDto>();
    }
}