using HappyChat.Application.Contracts;
using HappyChat.Application.Contracts.Repositories;

namespace HappyChat.Infrastructure.Persistance.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    #region Fields

    private readonly ApplicationDbContext _context;

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IUserChatRepository _userChatRepository;

    #endregion

    public UnitOfWork(
        ApplicationDbContext context,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IChatRepository chatRepository,
        IMessageRepository messageRepository,
        IContactRepository contactRepository,
        IUserChatRepository userChatRepository)
    {
        _context = context;

        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
        _contactRepository = contactRepository;
        _userChatRepository = userChatRepository;
    }

    public IUserRepository UserRepository => _userRepository;

    public IRoleRepository RoleRepository => _roleRepository;

    public IUserRoleRepository UserRoleRepository => _userRoleRepository;

    public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository;

    public IChatRepository ChatRepository => _chatRepository;

    public IMessageRepository MessageRepository => _messageRepository;

    public IContactRepository ContactRepository => _contactRepository;

    public IUserChatRepository UserChatRepository => _userChatRepository;

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}