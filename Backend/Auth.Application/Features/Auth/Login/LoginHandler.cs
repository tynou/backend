using Auth.Application.Interfaces;
using MediatR;

namespace Auth.Application.Features.Auth.Login;

public class LoginHandler(IUserRepository userRepository, IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);

        if (user is null)
            throw new Exception("User not found");

        return jwtProvider.Generate(user);
    }
}