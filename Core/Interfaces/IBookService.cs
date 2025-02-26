using Core.DTOs;
using Core.Enums;

namespace Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task<IEnumerable<BookDTO>> GetAllWithIncludesAsync();
        Task<IEnumerable<BookDTO>> GetBySpecificationAsync(string? title, Genre? genre, decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<BookDTO>> GetBySpecificationWithIncludesAsync(string? title, Genre? genre, decimal? minPrice, decimal? maxPrice);
        Task<BookDTO?> GetByIdAsync(int id);
        Task<BookDTO?> GetByIdWithIncludesAsync(int id);
        Task AddAsync(BookDTO bookDTO);
        Task UpdateAsync(BookDTO bookDTO);
        Task DeleteAsync(int id);
    }
}
