using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PopCinema.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        private DbSet<T> _db { get; set; }

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task<bool> CreateAsync(T entity)
        {
            try{
                await _db.AddAsync(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                return false;
            }
            
        }

        public async Task<bool> UpdateAsync(T entity) {
            try
            {
                _db.Update(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T entity) {
            try
            {
                _db.Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true) {
            IQueryable<T> entities = _db;

            if (expression is not null)
            {
                entities = entities.Where(expression);
            }

            if (include is not null)
            {
                    entities = include(entities);
            }

            if (!tracked)
            {
                entities = entities.AsNoTracking();
            }

            return (await entities.ToListAsync());

        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true) {
            return (await GetAsync(expression,include,tracked)).FirstOrDefault();
        }

        public async Task<bool> CommitAsync() {
            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
