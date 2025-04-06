using System.Linq.Expressions;

namespace Library.Model
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, object>> orderBy, int pageNumber = 1, int pageSize = 10);
        Task<T?> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        int Rows();
    }
}
