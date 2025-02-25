using Core.Entities;
using Core.Enums;

namespace Core.Specifications
{
    public class BookSpecification : BaseSpecification<Book>
    {
        public BookSpecification(string? title, Genre? genre, decimal? minPrice, decimal? maxPrice)
            : base(b =>
            (string.IsNullOrEmpty(title) || b.Title.Contains(title)) &&
            (!genre.HasValue || b.Genre == genre) &&
            (!minPrice.HasValue || b.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || b.Price <= maxPrice.Value))
        { }
    }
}
