using AutoMapper;
using BookLook.Application.Book.Dtos;

namespace BookLook.Application.Book.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Domain.Book.Book, BookDto>();
        }
    }
}
