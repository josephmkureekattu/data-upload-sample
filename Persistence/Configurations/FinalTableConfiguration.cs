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
    public class FinalTableConfiguration : IEntityTypeConfiguration<FinalTable>
    {
        public void Configure(EntityTypeBuilder<FinalTable> builder)
        {
            builder.Property(e => e.CreatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.UpdatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getutcdate())");
        }
    }
}
