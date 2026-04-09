using MassTransit;
using Notification.Application.Interfaces;
using Shared.Contracts.MQ;

namespace Notification.API.Consumers;

public class SendOtpConsumer(IEnumerable<INotificationProvider> providers) : IConsumer<SendOtpEvent>
{
    public async Task Consume(ConsumeContext<SendOtpEvent> context)
    {
        var provider = providers.FirstOrDefault(p => p.Type == context.Message.Type);
        
        if (provider is null)
            throw new NotSupportedException("Notification provider not found");
        
        await provider.SendAsync(context.Message.Identifier, context.Message.Code);
    }
}