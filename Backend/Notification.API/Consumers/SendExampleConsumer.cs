using System.Security.Cryptography;
using MassTransit;
using Shared.Contracts;

namespace Notification.API.Consumers;

public class SendExampleConsumer : IConsumer<SendExampleEvent>
{
    public async Task Consume(ConsumeContext<SendExampleEvent> context)
    {
        var message = context.Message;
        Console.WriteLine($"{RandomNumberGenerator.GetInt32(1, 100)} {message.Message}");
        
        await Task.CompletedTask;
    }
}