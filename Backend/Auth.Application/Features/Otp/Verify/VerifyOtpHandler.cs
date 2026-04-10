using MediatR;
using StackExchange.Redis;

namespace Auth.Application.Features.Otp.Verify;

public class VerifyOtpHandler(IConnectionMultiplexer redis) : IRequestHandler<VerifyOtpCommand, bool>
{
    private readonly IDatabase _redis = redis.GetDatabase();
    
    public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var cachedCode = await _redis.StringGetAsync($"otp:{request.Identifier}");

        if (cachedCode.IsNullOrEmpty || cachedCode != request.Code)
        {
            return false;
        }
        
        await _redis.KeyDeleteAsync($"otp:{request.Identifier}");
        return true;
    }
}