using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Contracts;
using PlatformService.Dtos;

namespace PlatformService.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
}