namespace HappyChat.Application.Features.UserFeatures.DTO;

public record RefreshResponse(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt);