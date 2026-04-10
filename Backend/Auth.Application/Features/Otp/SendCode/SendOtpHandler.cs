using System.Security.Cryptography;
using MassTransit;
using MediatR;
using Shared.Contracts;
using Shared.Contracts.MQ;
using StackExchange.Redis;

namespace Auth.Application.Features.Otp.SendCode;

public class SendOtpHandler(IPublishEndpoint publishEndpoint, IConnectionMultiplexer redis) : IRequestHandler<SendOtpCommand>
{
    private readonly IDatabase _redis = redis.GetDatabase();

    public async Task Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var identifier = "1rczhvwds@gmail.com";
        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        var otpEvent = new SendOtpEvent(NotificationType.Email, identifier, code);
        
        await _redis.StringSetAsync($"otp:{identifier}", code, TimeSpan.FromMinutes(5));
        
        await publishEndpoint.Publish(otpEvent, cancellationToken);
    }
}