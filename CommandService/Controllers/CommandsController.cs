using AutoMapper;
using CommandService.Data.Contracts;
using CommandService.DTOs;
using CommandService.Model;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/platforms/{platformId}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepository _commandRepository;
    private readonly IPlatformRepository _platformRepository;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepository commandRepository, IPlatformRepository platformRepository, IMapper mapper)
    {
        _commandRepository = commandRepository;
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
        if (_platformRepository.PlatformExists(platformId))
        {
            var platforms = _commandRepository.PlatformCommands(platformId);
        
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(platforms)); 
        }

        return NotFound();
    }

    [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
    {
        if (_platformRepository.PlatformExists(platformId))
        {
            var command = _commandRepository.PlatformCommands(platformId)
                                            .FirstOrDefault(item => item.Id == commandId);

            return Ok(_mapper.Map<CommandReadDto>(command)); 
        }

        return NotFound(); 
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(int platformId, CommandCreateDto commandReadDto)
    {
        if (_platformRepository.PlatformExists(platformId))
        {
            var command = _mapper.Map<Command>(commandReadDto);

            command.PlatformId = platformId;

            _commandRepository.Save(command); 
            _commandRepository.SaveChanges();

            var result = _mapper.Map<CommandReadDto>(command); 

            return CreatedAtRoute(nameof(CreateCommand), 
                 new { platformId = platformId, commandId = command.Id},
                 result); 
        }
        
        return NotFound(); 
    }
    
}