namespace UserService.Core.Services.Helper;

public interface IPasswordHasher
{
    string HashPassword(string password);
    
    bool VerifyPassword(string password, string hashedPassword);
}