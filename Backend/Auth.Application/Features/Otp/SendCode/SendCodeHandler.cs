using System.Security.Cryptography;
using MassTransit;
using MediatR;
using Shared.Contracts;
using Shared.Contracts.MQ;

namespace Auth.Application.Features.Otp.SendCode;

public class SendCodeHandler(IPublishEndpoint publishEndpoint): IRequestHandler<SendCodeCommand>
{
    public async Task Handle(SendCodeCommand request, CancellationToken cancellationToken)
    {
        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        var otpEvent = new SendOtpEvent(NotificationType.Email, "1rczhvwds@gmail.com", code);
        
        await publishEndpoint.Publish(otpEvent, cancellationToken);
    }
}