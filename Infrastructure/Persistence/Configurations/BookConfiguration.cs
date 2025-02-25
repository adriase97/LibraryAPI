using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            #region Primary Key
            builder.HasKey(a => a.Id);
            #endregion

            #region Properties Settings
            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(b => b.PublicationYear)
                   .IsRequired();

            builder.Property(b => b.ISBN)
                   .HasMaxLength(13)
                   .IsRequired();

            builder.Property(b => b.Pages)
            .IsRequired();

            builder.Property(b => b.Genre)
                .HasMaxLength(40)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(b => b.Price)
                   .HasPrecision(10, 2)
                   .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(b => b.Author)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.BookPublishers)
                   .WithOne(bp => bp.Book)
                   .HasForeignKey(bp => bp.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
