using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Contexts.DbConfigurations
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.ToTable("SubscriptionPlans");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.NameAr)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder.Property(x => x.NameEn)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder.Property(x => x.DescriptionAr)
                .HasMaxLength(500)
                .IsUnicode();

            builder.Property(x => x.DescriptionEn)
                .HasMaxLength(500)
                .IsUnicode();

            builder.Property(x => x.MonthlyPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.CurrencyCode)
                     .HasMaxLength(3)
                     .IsUnicode(false)
                     .HasDefaultValue("EGP")
                     .IsRequired();

            builder.Property(x => x.BranchLimit)
                .IsRequired(false);

            builder.Property(x => x.ProductLimit)
                .IsRequired(false);

            builder.Property(x => x.CashierLimit)
                .IsRequired(false);

            builder.Property(x => x.AllowVariants)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}
