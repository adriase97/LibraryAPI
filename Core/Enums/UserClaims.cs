namespace Core.Enums
{
    public enum UserClaims
    {
        // General access
        AuthorsAccess,
        BooksAccess,
        PublishersAccess,

        // Author Permissions
        AuthorsCreate,
        AuthorsEdit,
        AuthorsDelete,

        // Book Permissions
        BooksCreate,
        BooksEdit,
        BooksDelete,

        // Permissions for Publishers
        PublishersCreate,
        PublishersEdit,
        PublishersDelete
    }
}
