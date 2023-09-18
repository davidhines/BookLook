using BookLook.Domain.Book;

namespace BookLook.Infrastructure.Repository
{
    public sealed class InMemoryBookRepository : IRepositoryAsync<Book>
    {
        private readonly List<Book> _books = new();

        public Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            _books.Add(book);

            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            var books = (IReadOnlyCollection<Book>) _books.AsReadOnly();
            return Task.FromResult(books);
        }

        public Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = _books.FirstOrDefault(book => book.Id == id);
            return Task.FromResult(book);
        }

        public Task<IReadOnlyCollection<Book>> GetWhereAsync(Func<Book, bool> predicate, CancellationToken cancellationToken)
        {
            var books = (IReadOnlyCollection<Book>) _books
                .Where(predicate)
                .ToList()
                .AsReadOnly();

            return Task.FromResult(books);
        }

        public Task RemoveAsync(Book entity, CancellationToken cancellationToken)
        {
            _books.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Book entity, CancellationToken cancellationToken)
        {
            var book = _books.FirstOrDefault(book => book.Id == entity.Id);

            if (book != null)
            {
                book.Author = entity.Author;
                book.Title = entity.Title;
            }
            
            return Task.CompletedTask;
        }
    }
}