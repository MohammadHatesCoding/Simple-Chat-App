using HappyChat.Application.Contracts.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace HappyChat.Infrastructure.Services;

public class OTPService : IOTPService
{
    private readonly IConfiguration _configuration;
    public OTPService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public (string rawOtp, string hashedOtp) GenerateOtpAsync()
    {
        string rawOtp = RandomNumberGenerator.GetInt32(0, 1000000).ToString("D6");

        OtpHasher hasher = new OtpHasher(_configuration["OtpSecret:Secret"]);
        string computedHash = hasher.Hash(rawOtp);

        return (rawOtp, computedHash);
    }

    public bool VerifyOtpAsync(string storedOtpHash, string inputOtp)
    {
        OtpHasher hasher = new OtpHasher(_configuration["OtpSecret:Secret"]);
        
        var computedHash = hasher.Hash(inputOtp);

        var storedBytes = Convert.FromBase64String(storedOtpHash);
        var computedBytes = Convert.FromBase64String(computedHash);

        return CryptographicOperations.FixedTimeEquals(storedBytes, computedBytes);
    }
}

internal class OtpHasher
{
    private readonly byte[] _secretKey;
    internal OtpHasher(string base64Secret)
    {
        _secretKey = Convert.FromBase64String(base64Secret);
    }

    internal string Hash(string otp)
    {
        using var hmac = new HMACSHA256(_secretKey);
        var bytes = Encoding.UTF8.GetBytes(otp);
        var hash = hmac.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}
