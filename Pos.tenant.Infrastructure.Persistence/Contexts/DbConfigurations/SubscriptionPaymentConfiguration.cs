using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.tenant.Domain.Constants;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Contexts.DbConfigurations
{
    public class SubscriptionPaymentConfiguration : IEntityTypeConfiguration<SubscriptionPayment>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPayment> builder)
        {
            builder.ToTable("SubscriptionPayments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Method)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.ReferenceNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(x => x.Provider)
               .HasMaxLength(50);

            builder.Property(x => x.ProviderPaymentReference)
                .HasMaxLength(200);

            builder.Property(x => x.ProviderTransactionId)
                .HasMaxLength(200);

            builder.Property(x => x.ProviderStatus)
                .HasMaxLength(100);

            builder.Property(x => x.FailureReason)
                .HasMaxLength(500);

            builder.Property(x => x.IdempotencyKey)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(x => x.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue(PaymentStatuses.Pending)
                .IsRequired();

            builder.Property(x => x.PaidAt)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.HasIndex(x => new { x.TenantId, x.InvoiceId });

            builder.HasIndex(x => new { x.TenantId, x.Status });

            builder.HasIndex(x => x.IdempotencyKey)
                .IsUnique()
                .HasFilter("[IdempotencyKey] IS NOT NULL");

            builder.HasOne(x => x.Tenant)
                     .WithMany(x => x.SubscriptionPayments)
                     .HasForeignKey(x => x.TenantId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Invoice)
                    .WithMany(x => x.SubscriptionPayments)
                    .HasForeignKey(x => x.InvoiceId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
