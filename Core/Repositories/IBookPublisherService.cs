using Core.DTOs;

namespace Core.Repositories
{
    public interface IBookPublisherService
    {
        Task<IEnumerable<BookPublisherDTO>> GetAllAsync();
        Task<BookPublisherDTO?> GetByIdAsync(int bookId, int publisherId);
        Task AddAsync(BookPublisherDTO bookPublisherDTO);
        Task DeleteAsync(int bookId, int publisherId);
    }
}
