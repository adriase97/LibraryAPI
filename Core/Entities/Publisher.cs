namespace Core.Entities
{
    public class Publisher : BaseEntity
    {
        #region Properties
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int FoundedYear { get; set; }
        public string Website { get; set; } = string.Empty;
        #endregion

        #region Collection
        public ICollection<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();
        #endregion
    }
}
