using System.Linq.Expressions;
using Cybertek.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Cybertek.Repository;

public class GenericRepository<T> : IGenericRepository<T> 
    where T : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<T?> FindById(object?[]? keys, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(keys, cancellationToken);
    }

    public async Task<IList<T>> FindMany(IPaginator paginator,
        IList<Expression<Func<T, bool>>>? conditions = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null,
        bool? asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        int offset = paginator.PageNumber * paginator.Limit;
        int limit = paginator.Limit;

        IQueryable<T> queryable = _dbContext.Set<T>();
        if (conditions != null)
        {
            foreach (Expression<Func<T,bool>> condition in conditions)
            {
                queryable = queryable.Where(condition);
            }
        }

        if (includes != null)
        {
            queryable = includes(queryable);
        }

        if (asNoTracking.HasValue && asNoTracking == true)
        {
            queryable = queryable.AsNoTracking();
        }

        queryable = queryable
            .Skip(offset)
            .Take(limit);
        
        return await queryable.ToListAsync(cancellationToken);
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entityToDelete)
    {
        _dbSet.Remove(entityToDelete);
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
