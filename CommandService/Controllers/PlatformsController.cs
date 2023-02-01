using AutoMapper;
using CommandService.Data.Contracts;
using CommandService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public sealed class PlatformsController : ControllerBase
{
    private readonly ICommandRepository _commandRepository;
    private readonly IPlatformRepository _platformRepository;
    private readonly IMapper _mapper;

    public PlatformsController(ICommandRepository commandRepository, IPlatformRepository platformRepository, IMapper mapper)
    {
        _commandRepository = commandRepository;
        _platformRepository = platformRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("DEBUG: Retrieving platforms.");

        var platforms = _platformRepository.GetAll();
        
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms)); 
    }

    [HttpPost]
    public ActionResult Test()
    {
        Console.WriteLine("OK");

        return Ok(); 
    }
}