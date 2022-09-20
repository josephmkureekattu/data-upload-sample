using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class UploadLogConfiguration : IEntityTypeConfiguration<UploadLog>
    {
        public void Configure(EntityTypeBuilder<UploadLog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.file).WithMany(x => x.uploadLogs);
            builder.Property(e => e.CreatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.UpdatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getutcdate())");
        }
    }
}
