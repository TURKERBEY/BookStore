using Microsoft.EntityFrameworkCore;
using Shared.Core.Configurations.Common.Sessions;
using Shared.Core.Repositories.Base;
using Shared.Domain.Common;
using Shared.Persistence.Models;
using System.Diagnostics;
using System.Linq.Expressions;


namespace Shared.Persistence.Repositories.Base;
public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly BookStoreContext _context;
    protected readonly DbSet<T> _dbSet;
    private readonly ActivitySource _activitySource;

    public BaseRepository(BookStoreContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _activitySource = new ActivitySource(typeof(T).Name + "Repository");
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        Func<T, bool>? predicate = null,
        bool asNoTracking = true,
        string[]? includes = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (asNoTracking)
            query = query.AsNoTracking();

        if (includes is { Length: > 0 })
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        if (predicate != null)
            query = query.Where(predicate).AsQueryable(); // predicate uygulanıyor

        return await query.ToListAsync(cancellationToken);
    }
    public async Task<T?> GetSingleAsync(
    Func<T, bool> predicate,
    bool asNoTracking = true,
    string[]? includes = null,
    CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (asNoTracking)
            query = query.AsNoTracking();

        if (includes is { Length: > 0 })
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        return query.FirstOrDefault(predicate);
    }

    public async Task<T?> GetByIdAsync(int id, bool asNoTracking = true, string[]? includes = null, CancellationToken cancellationToken = default)
    {
        using var activity = _activitySource.StartActivity("GetByIdAsync");

        IQueryable<T> query = _dbSet.Where(x => x.Id == id);  // **IsRemove filtresi kaldırıldı**

        if (asNoTracking)
            query = query.AsNoTracking();

        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true, string[]? includes = null, CancellationToken cancellationToken = default)
    {
        using var activity = _activitySource.StartActivity("FindAsync");

        IQueryable<T> query = _dbSet.Where(predicate);  // **IsRemove filtresi kaldırıldı**

        if (asNoTracking)
            query = query.AsNoTracking();

        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        using var activity = _activitySource.StartActivity("AddAsync");

        entity.CreatedDate = DateTime.UtcNow;
        if (Session.UserId!=null)
        {
            entity.CreatedBy = Session.UserId ?? 0;
        }

        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        using var activity = _activitySource.StartActivity("UpdateAsync");

        entity.UpdatedDate = DateTime.UtcNow;
        if (Session.UserId != null)
        {
            entity.UpdatedBy = Session.UserId ?? 0;
        }
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, bool isSoftDelete = false, CancellationToken cancellationToken = default)
    {
        using var activity = _activitySource.StartActivity("DeleteAsync");

        if (isSoftDelete)
        {
            entity.IsRemove = true;
            entity.UpdatedDate = DateTime.UtcNow;
            _dbSet.Update(entity);
        }
        else
        {
            _dbSet.Remove(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var activity = _activitySource.StartActivity("SaveChangesAsync");

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
