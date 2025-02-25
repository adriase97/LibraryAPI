namespace Core.Entities
{
    public class Author : BaseEntity
    {
        #region Properties
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Biography { get; set; } = string.Empty;
        #endregion

        #region Collection
        public ICollection<Book> Books { get; set; } = new List<Book>();
        #endregion
    }
}
