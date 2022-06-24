using System.Linq.Expressions;

namespace Forward.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperies = null);
        T GetT(Expression<Func<T, bool>> predicate, string? includeProperies = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

    }
}
