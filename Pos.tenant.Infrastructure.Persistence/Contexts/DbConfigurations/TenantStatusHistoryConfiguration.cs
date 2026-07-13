using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Contexts.DbConfigurations
{
    public class TenantStatusHistoryConfiguration : IEntityTypeConfiguration<TenantStatusHistory>
    {
        public void Configure(EntityTypeBuilder<TenantStatusHistory> builder)
        {
            builder.ToTable("TenantStatusHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OldStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.NewStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Reason)
                .HasMaxLength(500)
                .IsUnicode();

            builder.Property(x => x.ChangedAt)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.HasIndex(x => new { x.TenantId, x.ChangedAt });

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
