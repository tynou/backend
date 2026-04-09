using System.Security.Cryptography;
using MassTransit;
using Notification.Application.Interfaces;
using Shared.Contracts;

namespace Notification.API.Consumers;

public class SendExampleConsumer(IEnumerable<INotificationProvider> providers) : IConsumer<SendExampleEvent>
{
    public async Task Consume(ConsumeContext<SendExampleEvent> context)
    {
        var provider = providers.FirstOrDefault(p => p.Type == NotificationType.Email);
        
        if (provider == null)
            throw new NotSupportedException("Notification provider not found");
        
        await provider.SendAsync(context.Message.Recipient, context.Message.Message);
        
        // var message = context.Message;
        // Console.WriteLine($"{RandomNumberGenerator.GetInt32(1, 100)} {message.Message}");
        //
        // await Task.CompletedTask;
    }
}