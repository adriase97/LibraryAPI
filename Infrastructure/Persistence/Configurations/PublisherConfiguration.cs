using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            #region Primary Key
            builder.HasKey(a => a.Id);
            #endregion

            #region Properties Settings
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Country)
                   .HasMaxLength(100);

            builder.Property(p => p.FoundedYear)
                   .IsRequired();

            builder.Property(p => p.Website)
                   .HasMaxLength(255);
            #endregion

            #region Relationship
            builder.HasMany(p => p.BookPublishers)
                   .WithOne(bp => bp.Publisher)
                   .HasForeignKey(bp => bp.PublisherId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
