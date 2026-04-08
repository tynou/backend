using System.Security.Cryptography;
using System.Text;
using Auth.Application.Interfaces;
using Konscious.Security.Cryptography;

namespace Auth.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int MemorySize = 65536; // 64 MB
    private const int DegreeOfParallelism = 4;
    private const int Iterations = 4; 
    private const int SaltSize = 16;
    private const int HashSize = 32;

    public byte[] CreateSalt() => RandomNumberGenerator.GetBytes(SaltSize);
    
    public string Hash(string password, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        
        using var argon2 = new Argon2id(passwordBytes);
        argon2.Salt = salt;
        argon2.DegreeOfParallelism = DegreeOfParallelism;
        argon2.MemorySize = MemorySize;
        argon2.Iterations = Iterations;

        var hash = argon2.GetBytes(HashSize);
        return Convert.ToHexString(hash);
    }

    public bool Verify(string password, byte[] salt, string hash)
    {
        return Hash(password, salt) == hash;
    }
}