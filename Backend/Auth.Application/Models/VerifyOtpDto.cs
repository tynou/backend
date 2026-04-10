namespace Auth.Application.Models;

public record VerifyOtpDto(string Type, string Identifier, string Code);