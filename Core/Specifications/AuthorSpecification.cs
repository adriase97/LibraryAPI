using Core.Entities;

namespace Core.Specifications
{
    public class AuthorSpecification : BaseSpecification<Author>
    {
        public AuthorSpecification(string? name)
            : base(b => string.IsNullOrEmpty(name) || b.Name.Contains(name)) { }
    }
}
