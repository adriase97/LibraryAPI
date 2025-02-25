using Core.Entities;

namespace Core.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<IEnumerable<Publisher>> GetAllWithIncludesAsync();
        Task<IEnumerable<Publisher>> GetBySpecificationWithIncludesAsync(ISpecification<Publisher> specification);
        Task<Publisher?> GetByIdWithIncludesAsync(int id);
    }
}
