using HappyChat.Core.Models;

namespace HappyChat.Application.Contracts.Repositories;

public interface IUserRoleRepository
{
    Task<List<UserRole>> GetAllAsync();
    Task CreateAsync(int UserId, int RoleId);
    Task DeleteAsync(int UserId, int RoleId);
    Task<List<UserRole>> GetUserRolesByUserIdAsync(int userId);
    Task<List<UserRole>> GetUserRolesByRoleIdAsync(int roleId);
}