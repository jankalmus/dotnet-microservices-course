using PlatformService.DTOs;

namespace PlatformService.DataServices.Asynchronous.MessageBus;

public interface IMessageBusClient
{
    void PublishPlatform(PlatformPublishedDto dto);
}