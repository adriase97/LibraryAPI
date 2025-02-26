using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Specifications;

namespace Core.Services
{
    public class AuthorService : IAuthorService
    {
        #region Fields
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllWithIncludesAsync()
        {
            var authors = await _authorRepository.GetAllWithIncludesAsync();
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }

        public async Task<IEnumerable<AuthorDTO>> GetBySpecificationAsync(string? name)
        {
            var specification = new AuthorSpecification(name);
            var authors = await _authorRepository.GetBySpecificationAsync(specification);
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }

        public async Task<IEnumerable<AuthorDTO>> GetBySpecificationWithIncludesAsync(string? name)
        {
            var specification = new AuthorSpecification(name);
            var authors = await _authorRepository.GetBySpecificationWithIncludesAsync(specification);
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }

        public async Task<AuthorDTO?> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) throw new AuthorException($"Author with ID {id} not found.");

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<AuthorDTO?> GetByIdWithIncludesAsync(int id)
        {
            var author = await _authorRepository.GetByIdWithIncludesAsync(id);
            if (author == null) throw new AuthorException($"Author with ID {id} not found.");

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task AddAsync(AuthorDTO authorDTO)
        {
            if (authorDTO == null) throw new AuthorException("Cannot add a null author.");

            var author = _mapper.Map<Author>(authorDTO);
            await _authorRepository.AddAsync(author);
        }

        public async Task UpdateAsync(AuthorDTO authorDTO)
        {
            if (authorDTO == null)
                throw new ArgumentNullException(nameof(authorDTO));

            var author = await _authorRepository.GetByIdAsync(authorDTO.Id);
            if (author == null)
                throw new AuthorException("Cannot update a non-existing author.");

            _mapper.Map(authorDTO, author);

            await _authorRepository.UpdateAsync(author);
        }

        public async Task DeleteAsync(int id)
        {
            _ = await _authorRepository.GetByIdAsync(id) ?? throw new AuthorException($"Cannot delete. Author with ID {id} not found.");
            await _authorRepository.DeleteAsync(id);
        }
        #endregion
    }
}
