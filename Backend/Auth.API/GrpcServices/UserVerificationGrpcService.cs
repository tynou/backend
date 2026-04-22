using Auth.Application.Interfaces;
using Grpc.Core;
using Common.Contracts.Grpc;

namespace Auth.API.GrpcServices;

public class UserVerificationGrpcService(IUserRepository userRepository) : UserVerificationGrpc.UserVerificationGrpcBase
{
    public override async Task<UserVerificationResponse> GetUserVerification(UserVerificationRequest request, ServerCallContext context)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        var response = new UserVerificationResponse()
        {
            IsVerified = user?.IsVerified ?? false,
            UserExists = user is not null,
        };
        return response;
    }
}