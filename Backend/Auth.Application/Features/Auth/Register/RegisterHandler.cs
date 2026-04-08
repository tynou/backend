using Auth.Application.Exceptions;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using MediatR;

namespace Auth.Application.Features.Auth.Register;

public class RegisterHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByUsernameAsync(request.Username);
        if (existingUser is not null)
            throw new ConflictException("Username already in use");

        var passwordSalt = passwordHasher.CreateSalt();
        var passwordHash = passwordHasher.Hash(request.Password, passwordSalt);
        
        var user = new User
        {
            Username = request.Username,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };
        await userRepository.AddAsync(user);
        
        return $"User {request.Username} registered successfully";
    }
}