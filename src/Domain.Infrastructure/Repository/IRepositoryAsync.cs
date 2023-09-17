using System.Linq.Expressions;

namespace BookLook.Infrastructure.Repository
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyCollection<T>> GetWhereAsync(Func<T, bool> predicate, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task RemoveAsync(T entity, CancellationToken cancellationToken);
    }
}
