namespace Core.DTOs
{
    public class BookPublisherDTO
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
        public DateTime PublishedDate { get; set; }
        public BookDTO? Book { get; set; }
        public PublisherDTO? Publisher { get; set; }
    }
}
