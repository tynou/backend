using MediatR;

namespace Auth.Application.Features.Auth.Login;

public record LoginCommand(string Username, string Password) : IRequest<string>;