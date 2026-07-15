namespace HappyChat.Application.Features.UserFeatures.DTO;

public record CheckOTPResponse(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt);