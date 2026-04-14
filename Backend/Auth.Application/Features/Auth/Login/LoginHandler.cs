using Auth.Application.Interfaces;
using Common.Application.Exceptions;
using MediatR;

namespace Auth.Application.Features.Auth.Login;

public class LoginHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher) : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);

        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordSalt, user.PasswordHash))
            throw new UnauthorizedException("Invalid username or password");

        return jwtProvider.Generate(user);
    }
}