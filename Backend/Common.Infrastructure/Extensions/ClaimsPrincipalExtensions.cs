using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Application.Exceptions;

namespace Common.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        
        if (string.IsNullOrEmpty(id))
            throw new UnauthorizedException("Incorrect JWT token.");
        
        if (!int.TryParse(id, out var userId))
            throw new UnauthorizedException("Invalid User ID format.");
        
        return userId;
    }

    public static string? GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
    }
}