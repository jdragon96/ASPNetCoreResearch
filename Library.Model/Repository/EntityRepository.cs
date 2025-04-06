using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Model
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _db;
        internal DbSet<T> dbSet;
        public EntityRepository(DatabaseContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async void Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public Task<T?> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            return query.Where(filter).FirstOrDefaultAsync();

        }

        public Task<List<T>> GetAll(Expression<Func<T, object>> orderBy, int pageNumber, int pageSize)
        {
            return dbSet
                .AsNoTracking()
                .OrderBy(orderBy)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public int Rows()
        {
            return dbSet.Count();
        }
    }
}
