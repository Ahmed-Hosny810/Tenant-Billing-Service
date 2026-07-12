using Pos.tenant.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Domain.Models
{
    public partial class SubscriptionPayment : BaseEntity
    {
        public Guid TenantId { get; set; }
        public Guid InvoiceId { get; set; }

        public decimal Amount { get; set; }

        public string Method { get; set; } = null!;
        public string? ReferenceNumber { get; set; }

        public string Status { get; set; } = PaymentStatuses.Completed;

        public DateTime PaidAt { get; set; }

        public void MarkCompleted()
        {
            Status = PaymentStatuses.Completed;
        }

        public void MarkFailed()
        {
            Status = PaymentStatuses.Failed;
        }

        public void Refund()
        {
            Status = PaymentStatuses.Refunded;
        }
    }
}
