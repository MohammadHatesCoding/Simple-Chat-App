using HappyChat.Application.Contracts.Repositories;
using HappyChat.Core.Models;

namespace HappyChat.Infrastructure.Persistance.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(ApplicationDbContext context)
        : base(context)
    {

    }
}