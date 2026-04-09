using System.Security.Cryptography;
using MassTransit;
using Shared.Contracts;

namespace Notification;

public class SendExampleConsumer : IConsumer<SendExampleEvent>
{
    public async Task Consume(ConsumeContext<SendExampleEvent> context)
    {
        var message = context.Message;
        // Здесь твоя логика: Console.WriteLine или вызов API СМС-шлюза
        Console.WriteLine($"{RandomNumberGenerator.GetInt32(1, 100)} {message.Message}");
        
        await Task.CompletedTask;
    }
}