namespace Common.Contracts.MQ;

public record SendOtpEvent(string Type, string Identifier, string Code);