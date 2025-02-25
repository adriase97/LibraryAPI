using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Specifications;

namespace Core.Services
{
    public class PublisherService : IPublisherService
    {
        #region Fields
        private readonly IPublisherRepository _publisherRepository;
        private readonly IBookPublisherRepository _bookPublisherRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PublisherService(IPublisherRepository publisherRepository, IBookPublisherRepository bookPublisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _bookPublisherRepository = bookPublisherRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<PublisherDTO>> GetAllAsync()
        {
            var publishers = await _publisherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PublisherDTO>>(publishers);
        }

        public async Task<IEnumerable<PublisherDTO>> GetAllWithIncludesAsync()
        {
            var publishers = await _publisherRepository.GetAllWithIncludesAsync();
            return _mapper.Map<IEnumerable<PublisherDTO>>(publishers);
        }

        public async Task<IEnumerable<PublisherDTO>> GetBySpecificationAsync(string? name, string? country)
        {
            var specification = new PublisherSpecification(name, country);
            var publishers = await _publisherRepository.GetBySpecificationAsync(specification);
            return _mapper.Map<IEnumerable<PublisherDTO>>(publishers);
        }

        public async Task<IEnumerable<PublisherDTO>> GetBySpecificationWithIncludesAsync(string? name, string? country)
        {
            var specification = new PublisherSpecification(name, country);
            var publishers = await _publisherRepository.GetBySpecificationWithIncludesAsync(specification);
            return _mapper.Map<IEnumerable<PublisherDTO>>(publishers);
        }

        public async Task<PublisherDTO?> GetByIdAsync(int id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null) throw new PublisherException($"Publisher with ID {id} not found.");

            return _mapper.Map<PublisherDTO>(publisher);
        }

        public async Task<PublisherDTO?> GetByIdWithIncludesAsync(int id)
        {
            var publisher = await _publisherRepository.GetByIdWithIncludesAsync(id);
            if (publisher == null) throw new PublisherException($"Publisher with ID {id} not found.");

            return _mapper.Map<PublisherDTO>(publisher);
        }

        public async Task AddAsync(PublisherDTO publisherDTO)
        {
            if (publisherDTO == null) throw new PublisherException("Cannot add a null publisher.");
            var publisher = _mapper.Map<Publisher>(publisherDTO);
            await _publisherRepository.AddAsync(publisher);
        }

        public async Task UpdateAsync(PublisherDTO publisherDTO)
        {
            if (publisherDTO == null) throw new PublisherException("Cannot add a null publisher.");
            var publisher = _mapper.Map<Publisher>(publisherDTO);

            await _bookPublisherRepository.DeleteByBookOrPublisherAsync(publisherId: publisher.Id);
            await _publisherRepository.UpdateAsync(publisher);

            if (publisherDTO.BookPublishers != null && publisherDTO.BookPublishers.Any())
            {
                var newRelations = publisherDTO.BookPublishers.Select(bp => new BookPublisher
                {
                    BookId = bp.BookId,
                    PublisherId = publisher.Id,
                    PublishedDate = bp.PublishedDate
                });

                await _bookPublisherRepository.AddRangeAsync(newRelations);
            }
        }

        public async Task DeleteAsync(int id)
        {
            _ = await _publisherRepository.GetByIdAsync(id) ?? throw new PublisherException($"Cannot delete. Publisher with ID {id} not found.");
            await _publisherRepository.DeleteAsync(id);
        }
        #endregion
    }
}
