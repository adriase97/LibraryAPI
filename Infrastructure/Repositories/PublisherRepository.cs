using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        public PublisherRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<Publisher?> GetByIdWithIncludesAsync(int id) => await _context.Publishers
            .Include(p => p.BookPublishers)
            .ThenInclude(bp => bp.Book)
            .ThenInclude(b => b.Author)
            .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Publisher>> GetAllWithIncludesAsync() => await _context.Publishers
            .Include(p => p.BookPublishers)
            .ThenInclude(bp => bp.Book)
            .ThenInclude(b => b.Author)
            .ToListAsync();

        public async Task<IEnumerable<Publisher>> GetBySpecificationWithIncludesAsync(ISpecification<Publisher> specification) => await _context.Set<Publisher>()
            .Where(specification.Criteria)
            .Include(p => p.BookPublishers)
            .ThenInclude(bp => bp.Book)
            .ThenInclude(b => b.Author)
            .ToListAsync();
        #endregion
    }
}
