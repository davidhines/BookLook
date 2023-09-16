using BookLook.Domain.Book;

namespace BookLook.Infrastructure.Repository
{
    public interface IInMemoryBookRepository
    {
        Task<Book?> GetAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Book book, CancellationToken cancellationToken);
    }
}
