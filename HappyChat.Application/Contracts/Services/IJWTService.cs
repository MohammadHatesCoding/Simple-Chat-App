using HappyChat.Core.Models;

namespace HappyChat.Application.Contracts.Services;

public interface IJWTService
{
    string GenerateAccessToken(User User);
    string GenerateRefreshToken();
}