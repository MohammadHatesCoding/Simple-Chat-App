using HappyChat.Application.Contracts.Repositories;
using HappyChat.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyChat.Infrastructure.Persistance.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByPhoneNumberWithRolesAsync(string phoneNumber)
    {
        return await _context.Users
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber && !x.IsDeleted);
    }
}
