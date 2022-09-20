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
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BatchIdentifier).IsRequired();
            builder.Property(e => e.CreatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.UpdatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getutcdate())");
        }
    }
}
