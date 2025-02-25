using Core.Entities;

namespace Core.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllWithIncludesAsync();
        Task<IEnumerable<Author>> GetBySpecificationWithIncludesAsync(ISpecification<Author> specification);
        Task<Author?> GetByIdWithIncludesAsync(int id);
    }
}
