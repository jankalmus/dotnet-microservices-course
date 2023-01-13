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
}