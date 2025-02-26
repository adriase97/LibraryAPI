using Core.Entities;

namespace Core.Interfaces
{
    public interface IBookPublisherRepository : IRepository<BookPublisher>
    {
        Task<BookPublisher?> GetByIdAsync(params object[] keyValues);
        Task DeleteByBookOrPublisherAsync(int? bookId = null, int? publisherId = null);
        Task AddRangeAsync(IEnumerable<BookPublisher> bookPublishers);
    }
}
