namespace Auth.Application.Interfaces;

public interface IPasswordHasher
{
    byte[] CreateSalt();
    
    string Hash(string password, byte[] salt);
    
    bool Verify(string password, byte[] salt, string hash);
}