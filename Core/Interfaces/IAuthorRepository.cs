using Core.Entities;

namespace Core.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllWithIncludesAsync();
        Task<IEnumerable<Author>> GetBySpecificationWithIncludesAsync(ISpecification<Author> specification);
        Task<Author?> GetByIdWithIncludesAsync(int id);
    }
}
