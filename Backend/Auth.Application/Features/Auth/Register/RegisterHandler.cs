using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Auth.Register;

public class RegisterHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByUsernameAsync(request.Username);
        if (existingUser is not null)
            throw new Exception("Username already in use");
        
        var user = new User
        {
            Username = request.Username,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = "hash",
        };
        await userRepository.AddAsync(user);
        
        return $"User {request.Username} registered successfully";
    }
}