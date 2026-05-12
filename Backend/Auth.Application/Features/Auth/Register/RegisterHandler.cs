using System.Diagnostics.Metrics;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Common.Application.Exceptions;
using MediatR;

namespace Auth.Application.Features.Auth.Register;

public class RegisterHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, Counter<long> registrationCounter) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByUsernameAsync(request.Username);
        if (existingUser is not null)
            throw new ConflictException("Username already in use");

        var passwordSalt = passwordHasher.CreateSalt();
        var passwordHash = passwordHasher.Hash(request.Password, passwordSalt);
        
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        await userRepository.AddAsync(user);
        
        registrationCounter.Add(1); 
    }
}