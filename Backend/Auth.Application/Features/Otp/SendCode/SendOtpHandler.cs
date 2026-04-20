using System.Security.Cryptography;
using MassTransit;
using MediatR;
using Shared.Contracts.MQ;
using StackExchange.Redis;

namespace Auth.Application.Features.Otp.SendCode;

public class SendOtpHandler(IPublishEndpoint publishEndpoint, IConnectionMultiplexer redis) : IRequestHandler<SendOtpCommand>
{
    private readonly IDatabase _redis = redis.GetDatabase();

    public async Task Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        await _redis.StringSetAsync($"otp:{request.Identifier}", code, TimeSpan.FromMinutes(5));
        
        var otpEvent = new SendOtpEvent("email", request.Identifier, code);
        await publishEndpoint.Publish(otpEvent, cancellationToken);
    }
}