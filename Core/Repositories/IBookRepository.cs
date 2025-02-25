using Core.Entities;

namespace Core.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllWithIncludesAsync();
        Task<IEnumerable<Book>> GetBySpecificationWithIncludesAsync(ISpecification<Book> specification);
        Task<Book?> GetByIdWithIncludesAsync(int id);
    }
}
