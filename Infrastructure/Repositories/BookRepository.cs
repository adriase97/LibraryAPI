using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<Book?> GetByIdWithIncludesAsync(int id) => await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookPublishers)
            .ThenInclude(bp => bp.Publisher)
            .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Book>> GetAllWithIncludesAsync() => await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookPublishers)
            .ThenInclude(bp => bp.Publisher)
            .ToListAsync();

        public async Task<IEnumerable<Book>> GetBySpecificationWithIncludesAsync(ISpecification<Book> specification) => await _context.Set<Book>()
            .Where(specification.Criteria)
            .Include(b => b.Author)
            .Include(b => b.BookPublishers)
            .ThenInclude(bp => bp.Publisher)
            .ToListAsync();

        #endregion
    }
}
