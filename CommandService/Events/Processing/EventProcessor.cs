using System.Text.Json;
using AutoMapper;
using CommandService.Data.Contracts;
using CommandService.DTOs;
using CommandService.Events.Shared;
using CommandService.Model;

namespace CommandService.Events.Processing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }
    
    public void ProcessEvent(string? message)
    {
        if (message is null) throw new ArgumentNullException(message);
        
        var eventType = IdentifyEvent(message);

        if (eventType == EventType.PlatformPublished)
        {
            SavePlatform(message);
        }

        if (eventType == EventType.Undetermined)
        {
            Console.WriteLine("INFO: Skipping unidentified event.");
        }
    }

    private void SavePlatform(string notificationMessage)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var serviceProvider = _scopeFactory.CreateScope().ServiceProvider;
            var repository = serviceProvider.GetService<IPlatformRepository>();
            var dto = JsonSerializer.Deserialize<PlatformPublishedDto>(notificationMessage); 

            try
            {
                var platform = _mapper.Map<Platform>(dto);

                if (repository!.ExternalPlatformExists(platform.ExternalId)) return;
                 
                repository.Save(platform); 
                repository.SaveChanges();
                
                Console.WriteLine("INFO: Platform added to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to add platform to the database. Details: {ex.Message}");
            }
        }
    }

    private static EventType IdentifyEvent(string notificationMessage)
    {
        var item = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        switch (item!.Event)
        {
            case "Platform_Published":
                return EventType.PlatformPublished;
            default:
                return EventType.Undetermined; 
        }
    }
}