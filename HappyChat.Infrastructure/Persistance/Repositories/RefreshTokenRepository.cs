using HappyChat.Application.Contracts.Repositories;
using HappyChat.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyChat.Infrastructure.Persistance.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private  readonly ApplicationDbContext _context;
    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(RefreshToken RefreshToken)
    {
        await _context.Set<RefreshToken>().AddAsync(RefreshToken);
    }

    public async Task DeleteExpiredTokensAsync()
    {
        var expiredTokens = await _context.Set<RefreshToken>().Where(x => x.ExpiresAt <= DateTime.UtcNow).ToListAsync();

        _context.Set<RefreshToken>().RemoveRange(expiredTokens);
    }

    public async Task<List<RefreshToken>> GetAllActiveTokensByUserId(int UserId)
    {
        var activeTokens = await _context.Set<RefreshToken>().Where(x => x.UserId == UserId && !x.IsRevoked && x.ExpiresAt > DateTime.UtcNow).ToListAsync();

        return activeTokens;
    }

    public async Task<RefreshToken> GetByTokenHashAsync(string Token)
    {
        return await _context.Set<RefreshToken>()
            .FirstOrDefaultAsync(x => x.Token == Token);
    }

    public Task RevokeAsync(RefreshToken Token)
    {
        Token.IsRevoked = true;
        return Task.CompletedTask;
    }

    public async Task<bool> UserHasActiveTokens(int UserId)
    {
        var result = await _context.Set<RefreshToken>().AnyAsync(x => x.UserId == UserId);
        return result;
    }
}
