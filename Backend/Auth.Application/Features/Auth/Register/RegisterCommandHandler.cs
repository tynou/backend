using MediatR;

namespace Auth.Application.Features.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return $"User {request.Username} registered successfully";
    }
}