namespace SecureApi.Models;

public class UserModel
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; } // In a real app, this should be a hashed password
    public required string Role { get; set; }
}