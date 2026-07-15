using System.Security.Cryptography;
using HappyChat.Application.Contracts.Services;

namespace HappyChat.Infrastructure.Services;

internal class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 11);

        return hash;
    }

    public bool VerifyPassword(string storedHash, string password)
    {
        try
        {
            var result = BCrypt.Net.BCrypt.Verify(password, storedHash);

            return result;
        }
        catch (Exception ex) 
        {
            throw new Exception(message: ex.Message);
        }
    }

    public string GeneratePasswordResetToken()
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }
}