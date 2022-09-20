using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Core.Entity.File;

namespace Persistence.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FileIdentifier).IsRequired();
            builder.HasOne(x => x.Batch).WithMany(x => x.files);
            builder.Property(e => e.CreatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.UpdatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getutcdate())");
        }
    }
}
