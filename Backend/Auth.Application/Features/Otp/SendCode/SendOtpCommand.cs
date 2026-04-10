using MediatR;

namespace Auth.Application.Features.Otp.SendCode;

public record SendOtpCommand(string Identifier) : IRequest;