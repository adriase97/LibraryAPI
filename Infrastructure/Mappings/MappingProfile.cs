using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping Author to AuthorDTO
            CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            CreateMap<AuthorDTO, Author>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            // Mapping Book to BookDTO
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.BookPublishers, opt => opt.MapFrom(src => src.BookPublishers));

            CreateMap<BookDTO, Book>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.BookPublishers, opt => opt.MapFrom(src => src.BookPublishers));

            // Mapping Publisher to PublisherDTO
            CreateMap<Publisher, PublisherDTO>();
            CreateMap<PublisherDTO, Publisher>();

            // Mapping BookPublisher to BookPublisherDTO
            CreateMap<BookPublisher, BookPublisherDTO>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));

            CreateMap<BookPublisherDTO, BookPublisher>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
        }
    }
}
