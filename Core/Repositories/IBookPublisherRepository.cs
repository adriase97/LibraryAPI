using Core.Entities;

namespace Core.Repositories
{
    public interface IBookPublisherRepository : IRepository<BookPublisher>
    {
        Task<BookPublisher?> GetByIdAsync(params object[] keyValues);
        Task DeleteByBookOrPublisherAsync(int? bookId = null, int? publisherId = null);
        Task AddRangeAsync(IEnumerable<BookPublisher> bookPublishers);
    }
}
