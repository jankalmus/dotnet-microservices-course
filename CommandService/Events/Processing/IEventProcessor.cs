namespace CommandService.Events.Processing;

public interface IEventProcessor
{
    void ProcessEvent(string message); 
}