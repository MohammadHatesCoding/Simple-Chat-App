using HappyChat.Application.Contracts.Repositories;
using HappyChat.Core.Models;

namespace HappyChat.Infrastructure.Persistance.Repositories;

public class UserChatRepository : BaseRepository<UserChat>, IUserChatRepository
{
    public UserChatRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}