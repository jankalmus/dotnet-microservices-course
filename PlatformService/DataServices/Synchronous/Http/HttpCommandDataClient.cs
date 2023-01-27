using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.DataServices.Synchronous.Http;

public sealed class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    
    public HttpCommandDataClient(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }
    
    public async Task SendPlatformToCommand(ReadPlatformDto dto)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(dto),
            Encoding.UTF8,
            "application/json"
            );
        
        var response = await _client.PostAsync($"{_configuration["CommandService"]}/api/c/platforms", httpContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("HttpClient OK");
        }
    }
}