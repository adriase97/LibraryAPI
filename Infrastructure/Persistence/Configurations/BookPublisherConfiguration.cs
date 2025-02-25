using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class BookPublisherConfiguration : IEntityTypeConfiguration<BookPublisher>
    {
        public void Configure(EntityTypeBuilder<BookPublisher> builder)
        {
            #region Primary Key
            builder.HasKey(bp => new { bp.BookId, bp.PublisherId });
            #endregion

            #region Properties Settings
            builder.Property(bp => bp.PublishedDate)
                   .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(bp => bp.Book)
                   .WithMany(b => b.BookPublishers)
                   .HasForeignKey(bp => bp.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bp => bp.Publisher)
                   .WithMany(p => p.BookPublishers)
                   .HasForeignKey(bp => bp.PublisherId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
