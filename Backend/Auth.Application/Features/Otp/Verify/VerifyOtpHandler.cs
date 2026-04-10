using Auth.Application.Interfaces;
using MediatR;
using StackExchange.Redis;

namespace Auth.Application.Features.Otp.Verify;

public class VerifyOtpHandler(IConnectionMultiplexer redis, IUserRepository userRepository) : IRequestHandler<VerifyOtpCommand, bool>
{
    private readonly IDatabase _redis = redis.GetDatabase();
    
    public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var cachedCode = await _redis.StringGetAsync($"otp:{request.Identifier}");

        if (cachedCode.IsNullOrEmpty || cachedCode.ToString() != request.Code)
            return false;
        
        await _redis.KeyDeleteAsync($"otp:{request.Identifier}");

        var user = await userRepository.GetByEmailAsync(request.Identifier);
        if (user is null)
            return false;

        await userRepository.SetVerifiedAsync(user, true);
        
        return true;
    }
}