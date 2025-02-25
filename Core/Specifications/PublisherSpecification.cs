using Core.Entities;

namespace Core.Specifications
{
    public class PublisherSpecification : BaseSpecification<Publisher>
    {
        public PublisherSpecification(string? name, string? country)
            : base(b =>
            (string.IsNullOrEmpty(name) || b.Name.Contains(name)) &&
            (string.IsNullOrEmpty(country) || b.Country.Contains(country)))
        { }
    }
}
