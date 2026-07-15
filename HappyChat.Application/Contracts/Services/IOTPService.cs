namespace HappyChat.Application.Contracts.Services;

public interface IOTPService
{
    (string rawOtp, string hashedOtp) GenerateOtpAsync();
    bool VerifyOtpAsync(string storedOtpHash, string inputOtp);
}