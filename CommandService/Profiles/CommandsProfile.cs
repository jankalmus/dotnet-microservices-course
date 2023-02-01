using CommandService.DTOs;
using CommandService.Model;

namespace CommandService.Profiles;

public class CommandsProfile : AutoMapper.Profile
{
    public CommandsProfile()
    {
        // Source --> Target
        CreateMap<CommandCreateDto, Command>();
        CreateMap<Command, CommandReadDto>();
    }
}