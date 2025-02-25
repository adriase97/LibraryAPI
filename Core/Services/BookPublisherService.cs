using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;

namespace Core.Services
{
    public class BookPublisherService : IBookPublisherService
    {
        #region Fields
        private readonly IBookPublisherRepository _bookPublisherRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BookPublisherService(IBookPublisherRepository bookPublisherRepository, IMapper mapper)
        {
            _bookPublisherRepository = bookPublisherRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<BookPublisherDTO>> GetAllAsync()
        {
            var bookPublishers = await _bookPublisherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookPublisherDTO>>(bookPublishers);
        }

        public async Task<BookPublisherDTO?> GetByIdAsync(int bookId, int publisherId)
        {
            var bookPublisher = await _bookPublisherRepository.GetByIdAsync(new object[] { bookId, publisherId });
            if (bookPublisher == null) throw new BookPublisherException($"No BookPublisher found with BookID {bookId} and PublisherID {publisherId}.");
            return _mapper.Map<BookPublisherDTO>(bookPublisher);
        }

        public async Task AddAsync(BookPublisherDTO bookPublisherDTO)
        {
            if (bookPublisherDTO == null) throw new BookPublisherException("Cannot add a null BookPublisher.");

            var bookPublisher = _mapper.Map<BookPublisher>(bookPublisherDTO);
            await _bookPublisherRepository.AddAsync(bookPublisher);
        }

        public async Task DeleteAsync(int bookId, int publisherId)
        {
            var bookPublisher = await _bookPublisherRepository.GetByIdAsync(new object[] { bookId, publisherId })
                ?? throw new BookPublisherException($"Cannot delete. No BookPublisher found with BookID {bookId} and PublisherID {publisherId}.");

            await _bookPublisherRepository.DeleteAsync(bookPublisher);
        }
        #endregion
    }
}
