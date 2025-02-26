using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        public AuthorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<Author?> GetByIdWithIncludesAsync(int id) => await _context.Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.BookPublishers)
            .ThenInclude(bp => bp.Publisher)
            .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Author>> GetAllWithIncludesAsync() => await _context.Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.BookPublishers)
            .ThenInclude(bp => bp.Publisher)
            .ToListAsync();

        public async Task<IEnumerable<Author>> GetBySpecificationWithIncludesAsync(ISpecification<Author> specification) => await _context.Authors
            .Where(specification.Criteria)
            .Include(a => a.Books)
            .ThenInclude(b => b.BookPublishers)
            .ThenInclude(bp => bp.Publisher)
            .ToListAsync();

        #endregion
    }
}
