using System.Linq.Expressions;
using Cybertek.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Cybertek.Repository
{
    public interface IGenericRepository<T> 
        where T : class
    {
        Task<T?> FindById(object?[]? keys, CancellationToken cancellationToken = default);

        Task<IList<T>> FindMany(IPaginator paginator,
            IList<Expression<Func<T, bool>>>? conditions = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null,
            bool? asNoTracking = false,
            CancellationToken cancellationToken = default);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void Remove(T entityToDelete);

        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}
