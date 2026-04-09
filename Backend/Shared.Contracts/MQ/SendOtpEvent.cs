namespace Shared.Contracts.MQ;

public record SendOtpEvent(NotificationType Type, string Identifier, string Code);