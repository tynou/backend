using MediatR;

namespace Auth.Application.Features.Otp.SendCode;

public record SendCodeCommand(string Identifier) : IRequest;