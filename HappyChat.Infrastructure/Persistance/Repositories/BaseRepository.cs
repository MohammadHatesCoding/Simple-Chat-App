using HappyChat.Application.Contracts.Repositories;
using HappyChat.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HappyChat.Infrastructure.Persistance.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        entity.UpdateDate = DateTime.UtcNow;
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return;
        entity.IsDeleted = true;
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
        => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    
    public virtual async Task<List<T>> GetAllAsync()
        => await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();

    public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _context.Set<T>().Where(x => !x.IsDeleted).Where(predicate).ToListAsync();

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        => await _context.Set<T>().Where(x => !x.IsDeleted).AnyAsync(predicate);
}