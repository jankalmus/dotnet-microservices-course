using AutoMapper;
using CommandService.Model;
using Grpc.Net.Client;
using PlatformService;

namespace CommandService.DataServices.gRPC;

public class PlatformDataClient : IPlatformDataClient
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _configuration = configuration;
    }
    
    public IEnumerable<Platform> ReturnAllPlatforms()
    {
        Console.WriteLine($"INFO: Initializing gRPC request to {_configuration["gRPCPlatform"]}");

        var channel = GrpcChannel.ForAddress(_configuration["gRPCPlatform"]!);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);

        var request = new GetAllRequest();

        try
        {
            var response = client.GetAllPlatforms(request);

            return _mapper.Map<IEnumerable<Platform>>(response.Platform); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: gRPC request failed. Info: {ex.Message}");
            throw; 
        }
    }
}