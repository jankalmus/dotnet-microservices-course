using AutoMapper;
using CommandService.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public sealed class PlatformsController : ControllerBase
{
    private readonly ICommandRepository _commandRepository;
    private readonly IMapper _mapper;

    public PlatformsController(ICommandRepository commandRepository, IMapper mapper)
    {
        _commandRepository = commandRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public ActionResult Test()
    {
        Console.WriteLine("OK");

        return Ok(); 
    }
}