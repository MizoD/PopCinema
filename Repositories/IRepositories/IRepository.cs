using System;
using System.Linq.Expressions;

namespace PopCinema.Repositories.IRepositories
{
    public interface IRepository<T>
    {
        Task<bool> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true);

        Task<T?> GetOneAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool tracked = true);

        Task<bool> CommitAsync();
    }
}
