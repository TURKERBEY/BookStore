using Shared.Domain.Common;
 
using System.Linq.Expressions;
 

namespace Shared.Core.Repositories.Base;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(
        Func<T, bool>? predicate = null,
        bool asNoTracking = true,
        string[]? includes = null,
        CancellationToken cancellationToken = default);

    Task<T> GetSingleAsync(
     Func<T, bool>? predicate = null,
     bool asNoTracking = true,
     string[]? includes = null,
     CancellationToken cancellationToken = default);


    Task<T?> GetByIdAsync(
        int id,
        bool asNoTracking = true,
        string[]? includes = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        bool asNoTracking = true,
        string[]? includes = null,
        CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, bool isSoftDelete = false, CancellationToken cancellationToken = default);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}