using Grpc.Core;
using Shared.Contracts.Grpc;

namespace Auth.API.GrpcServices;

public class UserVerificationGrpcService : UserVerificationGrpc.UserVerificationGrpcBase
{
    public override async Task<UserVerificationResponse> GetUserVerification(UserVerificationRequest request, ServerCallContext context)
    {
        // var user = await _db.Users.FindAsync(request.UserId);
        return new UserVerificationResponse();
    }
}