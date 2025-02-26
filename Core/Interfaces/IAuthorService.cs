using Core.DTOs;

namespace Core.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDTO>> GetAllAsync();
        Task<IEnumerable<AuthorDTO>> GetAllWithIncludesAsync();
        Task<IEnumerable<AuthorDTO>> GetBySpecificationAsync(string? name);
        Task<IEnumerable<AuthorDTO>> GetBySpecificationWithIncludesAsync(string? name);
        Task<AuthorDTO?> GetByIdAsync(int id);
        Task<AuthorDTO?> GetByIdWithIncludesAsync(int id);
        Task AddAsync(AuthorDTO authorDTO);
        Task UpdateAsync(AuthorDTO authorDTO);
        Task DeleteAsync(int id);
    }
}
