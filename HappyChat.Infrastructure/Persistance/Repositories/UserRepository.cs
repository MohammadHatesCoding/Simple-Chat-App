using HappyChat.Application.Contracts.Repositories;
using HappyChat.Core.Models;

namespace HappyChat.Infrastructure.Persistance.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
