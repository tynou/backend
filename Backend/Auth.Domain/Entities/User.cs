namespace Auth.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsVerified { get; set; }
    public string PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}