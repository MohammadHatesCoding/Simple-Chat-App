using HappyChat.Application.Contracts.Repositories;

namespace HappyChat.Application.Contracts;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    IRoleRepository RoleRepository { get; }

    IUserRoleRepository UserRoleRepository { get; }

    IRefreshTokenRepository RefreshTokenRepository { get; }

    IChatRepository ChatRepository { get; }

    IMessageRepository MessageRepository { get; }

    IContactRepository ContactRepository { get; }

    IUserChatRepository UserChatRepository { get; }

    Task CommitAsync(CancellationToken cancellationToken);
}