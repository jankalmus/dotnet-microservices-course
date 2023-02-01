using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Contracts;
using PlatformService.DataServices.Synchronous.Http;
using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient; 

    public PlatformsController(
        IPlatformRepository repository, IMapper mapper, ICommandDataClient commandDataClient)
    {
        _repository = repository;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
    }
    
    /// <summary>
    /// Returns all platforms.
    /// </summary>
    /// <returns>Returns all platforms.</returns>
    /// <remarks>
    /// Add remarks here. 
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ReadPlatformDto>> GetPlatforms()
    {
        var platforms = _repository.GetAll();

        return Ok(_mapper.Map<IEnumerable<ReadPlatformDto>>(platforms));  
    }
    
    /// <summary>
    /// Returns a platform by ID.
    /// </summary>
    /// <returns>OK with platform if platform exists. Not found if platform does not exist. </returns>
    /// <remarks>
    /// Add remarks here. 
    /// </remarks>
    [HttpGet("{id}", Name = "GetPlatformById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ReadPlatformDto> GetPlatformById(int id)
    {
        var platform = _repository.Get(id);

        if (platform is null) return NotFound();
        
        return Ok(_mapper.Map<ReadPlatformDto>(platform));  
    }

    
    /// <summary>
    /// Creates a new platform.
    /// </summary>
    /// <returns>Created (201) with created platform. </returns>
    /// <remarks>
    /// Sample request 
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ReadPlatformDto>> CreatePlatform(CreatePlatformDto dto)
    {
        var platform = _mapper.Map<Platform>(dto);

        var createdPlatform = _repository.Save(platform);
        
        _repository.SaveChanges();

        var readDto = _mapper.Map<ReadPlatformDto>(createdPlatform);

        try
        {
            await _commandDataClient.SendPlatformToCommand(readDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Encountered an error on HttpRequest. Fault: {ex.Message}");
        }

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = createdPlatform.Id }, readDto); 
    } 
}