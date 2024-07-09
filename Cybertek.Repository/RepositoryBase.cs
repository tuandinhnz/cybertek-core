using System.Linq.Expressions;
using Cybertek.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Cybertek.Repository;

public abstract class RepositoryBase<T> : IRepository<T>
    where T : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    protected RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public IQueryable<T> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<T?> FindById(object?[]? keys, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(keys, cancellationToken);
    }

    public async Task<IList<T>> FindMany(IPaginator paginator,
        Expression<Func<T, bool>>? condition = null,
        Expression<Func<T, object>>? orderBy = null,
        bool descending = false,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool? asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        int offset = (paginator.PageNumber - 1) * paginator.Limit;
        int limit = paginator.Limit;

        IQueryable<T> queryable = _dbContext.Set<T>();
        if (condition != null)
        {
            queryable = queryable.Where(condition);
        }

        if (include != null)
        {
            queryable = include(queryable);
        }

        if (orderBy != null)
        {
            queryable = descending ? queryable.OrderByDescending(orderBy) : queryable.OrderBy(orderBy);
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

    public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
