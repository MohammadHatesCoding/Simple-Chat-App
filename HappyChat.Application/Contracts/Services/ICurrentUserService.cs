namespace HappyChat.Application.Contracts.Services;

public interface ICurrentUserService
{
    int? UserId { get; }
    string? UserName { get; }
    string? Email { get; }
}