using MediatR;

namespace Auth.Application.Features.Auth.Register;

public record RegisterCommand(string Username, string Password, string Email) : IRequest;