namespace Core.Enums
{
    public enum Role
    {
        Admin,                  // You can access everything
        AuthorsBooksPublisher,  // You can access authors, books and publishers
        AuthorsBooks,           // You can access only authors and books
        Publisher,              // You can access only publishers
        ViewAuthorsBooks        // You can only see authors and books
    }
}
