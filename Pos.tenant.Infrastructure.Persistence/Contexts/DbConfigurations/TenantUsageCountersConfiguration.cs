using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Contexts.DbConfigurations
{
    public class TenantUsageCountersConfiguration : IEntityTypeConfiguration<TenantUsageCounters>
    {
        public void Configure(EntityTypeBuilder<TenantUsageCounters> builder)
        {
            builder.ToTable("TenantUsageCounters");

            builder.HasKey(x => x.TenantId);

            builder.Property(x => x.BranchCount)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.ProductCount)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.CashierCount)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.HasOne<Tenant>()
                .WithOne()
                .HasForeignKey<TenantUsageCounters>(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
