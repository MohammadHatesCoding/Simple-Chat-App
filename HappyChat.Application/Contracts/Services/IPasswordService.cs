namespace HappyChat.Application.Contracts.Services;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string storedHash, string password);
    string GeneratePasswordResetToken();
}