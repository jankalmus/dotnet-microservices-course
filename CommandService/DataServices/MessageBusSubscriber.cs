using System.Text;
using CommandService.Events.Processing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandService.DataServices;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;
         
        InitializeRabbitMq();
    }

    private string _queueName = default!;
    private IConnection _connection = default!;
    private IModel _channel = default!; 
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (ModuleHandle, ea) =>
        {
            Console.WriteLine("INFO: Received an event from RabbitMQ.");

            var body = ea.Body;
            var notficiationMessage = Encoding.UTF8.GetString(body.ToArray());

            _eventProcessor.ProcessEvent(notficiationMessage);

            Console.WriteLine("INFO: Processed an event from RabbitMQ.");
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask; 
    }
    
    private void InitializeRabbitMq()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMqHost"],
            Port = int.Parse(_configuration["RabbitMqPort"]!)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel(); 
        
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName; 
        
        _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
        _connection.ConnectionShutdown += LogConnectionShutdown; 
        
        Console.WriteLine("INFO: RabbitMQ connection set-up.");
    }

    private void LogConnectionShutdown(object? sender, ShutdownEventArgs args)
    {
        Console.WriteLine("INFO: RabbitMQ connection shut down. ");
    }
}