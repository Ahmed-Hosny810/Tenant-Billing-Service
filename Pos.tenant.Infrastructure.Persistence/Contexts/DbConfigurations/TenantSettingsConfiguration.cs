using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pos.tenant.Domain.Constants;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.Contexts.DbConfigurations
{
        public class TenantSettingsConfiguration : IEntityTypeConfiguration<TenantSettings>
        {
            public void Configure(EntityTypeBuilder<TenantSettings> builder)
            {
                builder.ToTable("TenantSettings");

                builder.HasKey(x => x.TenantId);

                builder.Property(x => x.DefaultVatRate)
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValue(14)
                    .IsRequired();

                builder.Property(x => x.PricesIncludeTax)
                    .HasDefaultValue(false)
                    .IsRequired();

                builder.Property(x => x.ReceiptFooterAr)
                    .HasMaxLength(500)
                    .IsUnicode();

                builder.Property(x => x.ReceiptFooterEn)
                    .HasMaxLength(500)
                    .IsUnicode();

                builder.Property(x => x.AllowReturns)
                    .HasDefaultValue(true)
                    .IsRequired();

                builder.Property(x => x.DiscountLimitPercent)
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValue(20)
                    .IsRequired();

                builder.Property(x => x.DefaultLanguage)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValue(SupportedLanguages.English)
                    .IsRequired();

                builder.HasOne<Tenant>()
                    .WithOne()
                    .HasForeignKey<TenantSettings>(x => x.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }

