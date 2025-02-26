using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookPublisherRepository : Repository<BookPublisher>, IBookPublisherRepository
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        public BookPublisherRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<BookPublisher?> GetByIdAsync(params object[] keyValues) => await _context.BookPublishers.FindAsync(keyValues);

        public async Task DeleteByBookOrPublisherAsync(int? bookId = null, int? publisherId = null)
        {
            if (bookId == null && publisherId == null) return;

            var query = _context.BookPublishers.AsQueryable();

            if (bookId.HasValue) query = query.Where(bp => bp.BookId == bookId.Value);
            if (publisherId.HasValue) query = query.Where(bp => bp.PublisherId == publisherId.Value);

            var bookPublishersToDelete = await query.ToListAsync();

            if (!bookPublishersToDelete.Any()) return; // There are no relationships to delete

            _context.BookPublishers.RemoveRange(bookPublishersToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<BookPublisher> bookPublishers)
        {
            await _context.BookPublishers.AddRangeAsync(bookPublishers);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
