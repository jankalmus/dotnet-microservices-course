using System.Text;
using System.Text.Json;
using PlatformService.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace PlatformService.DataServices.Asynchronous.MessageBus;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConnection _connection;
    private readonly IModel _channel; 
    
    public MessageBusClient(IConfiguration configuration)
    {
        var factory = new ConnectionFactory()
        {
            HostName = configuration["RabbitMQHost"],
            Port = int.Parse(configuration["RabbitMQPort"]!)
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel(); 
            
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += ConnectionShutdown; 
            
            Console.WriteLine("INFO: Message bus connection established.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"ERROR: Failed to set-up RabbitMQ connection. Details: ({e.Message})");
            
            throw new ConnectFailureException("Failed to establish connection.", e);
        }
    }
    
    public void PublishPlatform(PlatformPublishedDto dto)
    {
        var message  = JsonSerializer.Serialize(dto);

        if (_connection.IsOpen)
        {
            SendRabbitMqMessage(message);
        } 
        else
        {
            Console.WriteLine($"ERROR: Failed to publish {typeof(PlatformPublishedDto)}. Connection is inactive.");
        }
    }

    private void SendRabbitMqMessage(string? message)
    {
        if (message is null) throw new ArgumentNullException(message); 
        
        var body = Encoding.UTF8.GetBytes(message); 
        
         _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
         
         Console.WriteLine("INFO: Published a message.");
    }

    private void ConnectionShutdown(object? sender, ShutdownEventArgs args)
    {
        Console.WriteLine("INFO: RabbitMQ connection shutdown.");
    }
}