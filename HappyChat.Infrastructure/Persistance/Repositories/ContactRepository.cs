using HappyChat.Application.Contracts.Repositories;
using HappyChat.Core.Models;

namespace HappyChat.Infrastructure.Persistance.Repositories;

public class ContactRepository : BaseRepository<Contact>, IContactRepository
{
    public ContactRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}