using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Specifications;

namespace Core.Services
{
    public class BookService : IBookService
    {
        #region Fields
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var book = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(book);
        }

        public async Task<IEnumerable<BookDTO>> GetAllWithIncludesAsync()
        {
            var book = await _bookRepository.GetAllWithIncludesAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(book);
        }

        public async Task<IEnumerable<BookDTO>> GetBySpecificationAsync(string? title, Genre? genre, decimal? minPrice, decimal? maxPrice)
        {
            var specification = new BookSpecification(title, genre, minPrice, maxPrice);
            var book = await _bookRepository.GetBySpecificationAsync(specification);
            return _mapper.Map<IEnumerable<BookDTO>>(book);
        }

        public async Task<IEnumerable<BookDTO>> GetBySpecificationWithIncludesAsync(string? title, Genre? genre, decimal? minPrice, decimal? maxPrice)
        {
            var specification = new BookSpecification(title, genre, minPrice, maxPrice);
            var book = await _bookRepository.GetBySpecificationWithIncludesAsync(specification);
            return _mapper.Map<IEnumerable<BookDTO>>(book);
        }

        public async Task<BookDTO?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new BookException($"Book with ID {id} not found.");

            return _mapper.Map<BookDTO?>(book);
        }

        public async Task<BookDTO?> GetByIdWithIncludesAsync(int id)
        {
            var book = await _bookRepository.GetByIdWithIncludesAsync(id);
            if (book == null) throw new BookException($"Book with ID {id} not found.");

            return _mapper.Map<BookDTO?>(book);
        }

        public async Task AddAsync(BookDTO bookDTO)
        {
            if (bookDTO == null) throw new BookException("Cannot add a null book.");
            var book = _mapper.Map<Book>(bookDTO);
            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateAsync(BookDTO bookDTO)
        {
            if (bookDTO == null)
                throw new ArgumentNullException(nameof(bookDTO));

            var book = await _bookRepository.GetByIdAsync(bookDTO.Id);
            if (book == null)
                throw new BookException("Cannot update a non-existing book.");

            _mapper.Map(bookDTO, book);

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteAsync(int id)
        {
            _ = await _bookRepository.GetByIdAsync(id) ?? throw new BookException($"Cannot delete. Book with ID {id} not found.");
            await _bookRepository.DeleteAsync(id);
        }
        #endregion
    }
}
