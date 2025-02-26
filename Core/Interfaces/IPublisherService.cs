using Core.DTOs;

namespace Core.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherDTO>> GetAllAsync();
        Task<IEnumerable<PublisherDTO>> GetAllWithIncludesAsync();
        Task<IEnumerable<PublisherDTO>> GetBySpecificationAsync(string? name, string? country);
        Task<IEnumerable<PublisherDTO>> GetBySpecificationWithIncludesAsync(string? name, string? country);
        Task<PublisherDTO?> GetByIdAsync(int id);
        Task<PublisherDTO?> GetByIdWithIncludesAsync(int id);
        Task AddAsync(PublisherDTO publisherDTO);
        Task UpdateAsync(PublisherDTO publisherDTO);
        Task DeleteAsync(int id);
    }
}
