using Core.Enums;

namespace Core.Entities
{
    public class Book : BaseEntity
    {
        #region Properties
        public string Title { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int Pages { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        #endregion

        #region Navigation
        public Author Author { get; set; } = null!;
        #endregion

        #region Collection
        public ICollection<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();
        #endregion
    }
}
