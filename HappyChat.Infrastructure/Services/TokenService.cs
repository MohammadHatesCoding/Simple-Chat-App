using HappyChat.Application.Contracts.Services;
using System.Security.Cryptography;
using System.Text;

namespace HappyChat.Infrastructure.Services;

public class TokenService : ITokenService
{
    public string Hash(string Token)
    {
        if (string.IsNullOrWhiteSpace(Token))
            throw new ArgumentException("Token cannot be null or empty.");

        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(Token);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    public bool Verify(string RawToken, string StoredHash)
    {
        var hashedInput = Hash(RawToken);

        // جلوگیری از Timing Attack
        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(hashedInput),
            Encoding.UTF8.GetBytes(StoredHash)
        );
    }
}
