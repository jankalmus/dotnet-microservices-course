using PlatformService.DTO;

namespace PlatformService.DataServices.Synchronous.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(ReadPlatformDto dto); 
}