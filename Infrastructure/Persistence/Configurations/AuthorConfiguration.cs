using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            #region Primary Key
            builder.HasKey(a => a.Id);
            #endregion

            #region Properties Settings
            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Nationality)
                   .HasMaxLength(50);

            builder.Property(a => a.BirthDate)
                   .IsRequired();

            builder.Property(a => a.Biography)
                   .HasMaxLength(1000);
            #endregion
        }
    }
}
