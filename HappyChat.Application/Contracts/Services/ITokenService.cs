namespace HappyChat.Application.Contracts.Services;

public interface ITokenService
{
    string Hash(string Token);
    bool Verify(string RawToken, string StoredHash);
}
