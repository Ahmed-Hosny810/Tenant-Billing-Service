using Microsoft.EntityFrameworkCore;
using Pos.tenant.Application.Features.Tenants.Queries.GetAllQuery;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.QueryExtensions
{
    public static class TenantQueryExtensions
    {
        public static IQueryable<Tenant> ApplyFilters(
            this IQueryable<Tenant> query,
            TenantFilter? filter)
        {
            if (filter == null)
                return query;

            if (filter.Id.HasValue)
                query = query.Where(x => x.Id == filter.Id.Value);

            if (!string.IsNullOrWhiteSpace(filter.NameEn))
            {
                var nameEn = filter.NameEn.Trim().ToLower();

                query = query.Where(x => x.NameEn.ToLower().Contains(nameEn));
            }

            if (!string.IsNullOrWhiteSpace(filter.NameAr))
            {
                var nameAr = filter.NameAr.Trim().ToLower();

                query = query.Where(x => x.NameAr != null &&
                                         x.NameAr.ToLower().Contains(nameAr));
            }

            if (!string.IsNullOrWhiteSpace(filter.BusinessTypeCode))
            {
                var businessTypeCode = filter.BusinessTypeCode.Trim().ToUpperInvariant();

                query = query.Where(x => x.BusinessTypeCode == businessTypeCode);
            }

            if (!string.IsNullOrWhiteSpace(filter.Status))
            {
                var status = filter.Status.Trim();

                query = query.Where(x => x.Status == status);
            }

            if (!string.IsNullOrWhiteSpace(filter.CurrencyCode))
            {
                var currencyCode = filter.CurrencyCode.Trim().ToUpperInvariant();

                query = query.Where(x => x.CurrencyCode == currencyCode);
            }

            if (!string.IsNullOrWhiteSpace(filter.InventoryMode))
            {
                var inventoryMode = filter.InventoryMode.Trim();

                query = query.Where(x => x.InventoryMode == inventoryMode);
            }

            return query;
        }

        public static IQueryable<Tenant> ApplyIncludes(
            this IQueryable<Tenant> query,
            TenantIncludes? includes)
        {
            if (includes == null)
                return query;

            if (includes.TenantSettings)
            {
                query = query.Include(x => x.TenantSettings);
            }

            if (includes.TenantUsageCounters)
            {
                query = query.Include(x => x.TenantUsageCounters);
            }

            if (includes.TenantSubscription)
            {
                query = query.Include(x => x.TenantSubscriptions)
                             .ThenInclude(x => x.Plan);
            }

            return query;
        }

        public static IQueryable<Tenant> ApplyOrdering(
            this IQueryable<Tenant> query,
            TenantOrderKey orderKey,
            bool orderDescending)
        {
            return orderKey switch
            {
                TenantOrderKey.NameAr => orderDescending
                    ? query.OrderByDescending(x => x.NameAr)
                    : query.OrderBy(x => x.NameAr),

                TenantOrderKey.NameEn => orderDescending
                    ? query.OrderByDescending(x => x.NameEn)
                    : query.OrderBy(x => x.NameEn),

                TenantOrderKey.Status => orderDescending
                    ? query.OrderByDescending(x => x.Status)
                    : query.OrderBy(x => x.Status),

                TenantOrderKey.CurrencyCode => orderDescending
                    ? query.OrderByDescending(x => x.CurrencyCode)
                    : query.OrderBy(x => x.CurrencyCode),

                TenantOrderKey.InventoryMode => orderDescending
                    ? query.OrderByDescending(x => x.InventoryMode)
                    : query.OrderBy(x => x.InventoryMode),

                TenantOrderKey.UpdatedAt => orderDescending
                    ? query.OrderByDescending(x => x.UpdatedAt)
                    : query.OrderBy(x => x.UpdatedAt),

                TenantOrderKey.CreatedAt => orderDescending
                    ? query.OrderByDescending(x => x.CreatedAt)
                    : query.OrderBy(x => x.CreatedAt),

                _ => query.OrderByDescending(x => x.CreatedAt)
            };
        }
    }
}
