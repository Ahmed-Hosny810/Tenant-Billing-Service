using Pos.tenant.Domain.Common;
using Pos.tenant.Domain.Constants;
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

        public string Status { get; set; } = PaymentStatuses.Pending;

        public DateTime? PaidAt { get; set; }

        public void MarkCompleted(DateTime paidAt)
        {
            Status = PaymentStatuses.Completed;
            PaidAt = paidAt;
        }

        public void MarkFailed()
        {
            Status = PaymentStatuses.Failed;
            PaidAt = null;
        }

        public void Refund()
        {
            Status = PaymentStatuses.Refunded;
        }
    }
}
