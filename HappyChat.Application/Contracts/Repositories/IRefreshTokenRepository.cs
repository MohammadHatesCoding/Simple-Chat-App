using HappyChat.Core.Models;

namespace HappyChat.Application.Contracts.Repositories;

public interface IRefreshTokenRepository
{
    Task CreateAsync(RefreshToken RefreshToken);
    Task<List<RefreshToken>> GetAllActiveTokensByUserId(int UserId);
    Task<RefreshToken> GetByTokenHashAsync(string Token);
    Task RevokeAsync(RefreshToken Token);
    Task DeleteExpiredTokensAsync();
    Task<bool> UserHasActiveTokens(int UserId);
}