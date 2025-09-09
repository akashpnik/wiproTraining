using SecureApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecureApi.Services;

public class UserService
{
    // In-memory user database for demonstration purposes
    private readonly List<UserModel> _users = new()
    {
        new UserModel { Id = 1, Username = "admin", Password = "adminpassword", Role = "Admin" },
        new UserModel { Id = 2, Username = "user", Password = "userpassword", Role = "User" }
    };

    public UserModel? GetUser(string username, string password)
    {
        return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
}