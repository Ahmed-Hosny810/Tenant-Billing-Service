using Pos.tenant.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionInvoices.Queries.GetAllQuery
{
    public class GetAllTenantInvoicesQueryParameter:RequestParameter<SubscriptionInvoiceOrderKey>
    {
        public SubscriptionInvoiceFilter? Filter { get; set; }
    }
    public class SubscriptionInvoiceFilter
    {
        public string? Status { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? PeriodStartFrom { get; set; }
    }

    public enum SubscriptionInvoiceOrderKey
    {
        DueDate,
        CreatedAt,
        PeriodStart,
        PeriodEnd,
        Total,
        Status
    }
}
