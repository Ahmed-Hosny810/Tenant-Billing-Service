using Pos.tenant.Application.Features.SubscriptionInvoices.Queries.GetAllQuery;
using Pos.tenant.Application.Features.Tenants.Queries.GetAllQuery;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Infrastructure.Persistence.QueryExtensions
{
    public static class SubscriptionInvoiceQueryExtensions
    {
        public static IQueryable<SubscriptionInvoice> ApplyFilters(this IQueryable<SubscriptionInvoice> query,SubscriptionInvoiceFilter? filter)
        {
            if (filter == null)
                return query;

            if (!string.IsNullOrWhiteSpace(filter.Status))
            {
                var status = filter.Status.Trim();

                query = query.Where(x => x.Status == status);
            }

            if (filter.DueDate.HasValue)
            {
                var dueDate = filter.DueDate.Value.Date;
                var nextDay = dueDate.AddDays(1);

                query = query.Where(x => x.DueDate >= dueDate && x.DueDate < nextDay);
            }

            if (filter.PeriodStartFrom.HasValue)
            {
                query = query.Where(x => x.PeriodStart >= filter.PeriodStartFrom.Value);
            }

            return query;
        }

        public static IQueryable<SubscriptionInvoice> ApplyOrdering(
            this IQueryable<SubscriptionInvoice> query,
            SubscriptionInvoiceOrderKey orderKey,
            bool orderDescending)
        {
            return orderKey switch
            {
                SubscriptionInvoiceOrderKey.DueDate => orderDescending
                    ? query.OrderByDescending(x => x.DueDate)
                    : query.OrderBy(x => x.DueDate),

                SubscriptionInvoiceOrderKey.PeriodEnd => orderDescending
                    ? query.OrderByDescending(x => x.PeriodEnd)
                    : query.OrderBy(x => x.PeriodEnd),

                SubscriptionInvoiceOrderKey.PeriodStart => orderDescending
                    ? query.OrderByDescending(x => x.PeriodStart)
                    : query.OrderBy(x => x.PeriodStart),

                SubscriptionInvoiceOrderKey.Total => orderDescending
                    ? query.OrderByDescending(x => x.Total)
                    : query.OrderBy(x => x.Total),

                SubscriptionInvoiceOrderKey.Status => orderDescending
                    ? query.OrderByDescending(x => x.Status)
                    : query.OrderBy(x => x.Status),

                _ => query.OrderByDescending(x => x.CreatedAt)
            };
        }

    }
}
