using HappyChat.Application.Contracts.Repositories;
using HappyChat.Core.Models;

namespace HappyChat.Infrastructure.Persistance.Repositories;

public class ChatRepository : BaseRepository<Chat>, IChatRepository
{
    public ChatRepository(ApplicationDbContext context) : base(context) { }
}
