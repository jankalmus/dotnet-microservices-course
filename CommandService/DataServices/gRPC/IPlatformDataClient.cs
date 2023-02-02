using CommandService.Model;

namespace CommandService.DataServices.gRPC;

public interface IPlatformDataClient
{
    IEnumerable<Platform> ReturnAllPlatforms(); 
}