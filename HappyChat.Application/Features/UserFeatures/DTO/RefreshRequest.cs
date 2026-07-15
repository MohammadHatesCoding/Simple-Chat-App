namespace HappyChat.Application.Features.UserFeatures.DTO;

public record RefreshRequest(string RefreshToken, int UserId);