using Auth.Domain.Entities;

namespace Auth.Application.Interfaces;

public interface IJwtProvider
{
    string Generate(User user);
}