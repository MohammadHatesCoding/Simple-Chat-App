using HappyChat.Core.Models;
using System.Linq.Expressions;

namespace HappyChat.Application.Contracts.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}
