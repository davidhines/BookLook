using AutoMapper;
using BookLook.Application.Book.Dtos;
using BookLook.Infrastructure.Repository;
using MediatR;

namespace BookLook.Application.Book.Queries
{
    public sealed class GetBook : IRequest<BookDto>
    {
        public int Id { get; set; }

        public GetBook(int id)
        {
            Id = id;
        }

        internal sealed class GetBookHandler : IRequestHandler<GetBook, BookDto>
        {
            private readonly IRepositoryAsync<Domain.Book.Book> _bookRepository;
            private readonly IMapper _mapper;

            public GetBookHandler(IRepositoryAsync<Domain.Book.Book> bookRepository, IMapper mapper)
            {
                _bookRepository = bookRepository;
                _mapper = mapper;
            }

            public async Task<BookDto> Handle(GetBook request, CancellationToken cancellationToken)
            {
                var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
                return _mapper.Map<BookDto>(book);
            }
        }
    }
}
