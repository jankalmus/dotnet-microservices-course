using PlatformService.Dtos;

namespace PlatformService.DataServices.Synchronous.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(ReadPlatformDto dto); 
}