using MediatR;

namespace Auth.Application.Features.Otp.Verify;

public record VerifyOtpCommand(string Identifier, string Code) : IRequest<bool>;