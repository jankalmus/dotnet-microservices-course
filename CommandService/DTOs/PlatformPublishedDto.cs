namespace CommandService.DTOs;

public class PlatformPublishedDto : GenericEventDto
{
    public string Id { get; set; } = default!;

    public string Name { get; set; } = default!;
}