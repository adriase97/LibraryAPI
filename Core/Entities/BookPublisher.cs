namespace Core.Entities
{
    public class BookPublisher
    {
        #region Properties
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public DateTime PublishedDate { get; set; }
        #endregion

        #region Navigations
        public Book Book { get; set; } = null!;
        public Publisher Publisher { get; set; } = null!;
        #endregion
    }
}
