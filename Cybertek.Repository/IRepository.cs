using System.Linq.Expressions;
using Cybertek.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Cybertek.Repository
{
    public interface IRepository<T> 
        where T : class
    {
        IQueryable<T> GetQueryable();

        Task<T?> FindById(object?[]? keys, CancellationToken cancellationToken = default);

        Task<IList<T>> FindMany(IPaginator paginator,
            Expression<Func<T, bool>>? conditions = null,
            Expression<Func<T, object>>? orderBy = null,
            bool descending = false,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null,
            bool? asNoTracking = false,
            CancellationToken cancellationToken = default);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void Remove(T entityToDelete);

        Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }
}
