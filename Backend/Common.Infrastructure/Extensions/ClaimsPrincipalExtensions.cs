using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Common.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    }

    public static string? GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
    }
}