using BookLook.Domain.Book;

namespace BookLook.Infrastructure.Repository
{
    public sealed class InMemoryBookRepository : IInMemoryBookRepository
    {
        private readonly List<Book> _books = new();
        private int _primaryKey = 1;

        public Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            book.Id = _primaryKey++;
            _books.Add(book);

            return Task.CompletedTask;
        }

        public Task<Book?> GetAsync(int id, CancellationToken cancellationToken)
        {
            var book = _books.FirstOrDefault(book => book.Id == id);

            return Task.FromResult(book);
        }
    }
}
