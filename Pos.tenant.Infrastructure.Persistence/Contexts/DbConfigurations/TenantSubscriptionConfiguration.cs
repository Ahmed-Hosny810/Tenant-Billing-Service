using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.tenant.Domain.Constants;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Contexts.DbConfigurations
{
    public class TenantSubscriptionConfiguration : IEntityTypeConfiguration<TenantSubscription>
    {
        public void Configure(EntityTypeBuilder<TenantSubscription> builder)
        {
            builder.ToTable("TenantSubscriptions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue(SubscriptionStatuses.Active)
                .IsRequired();

            builder.Property(x => x.CurrentPeriodStart)
                .IsRequired();

            builder.Property(x => x.CurrentPeriodEnd)
                .IsRequired();

            builder.Property(x => x.GracePeriodEndsAt)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.HasIndex(x => x.TenantId);

            builder.HasIndex(x => new { x.TenantId, x.Status });

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<SubscriptionPlan>()
                .WithMany()
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
