using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPayments.DTOS
{
    public class PaymobCreateIntentionRequest
    {
        public Guid PaymentId { get; set; }

        public string InvoiceId { get; set; } = null!;
        public string InvoiceNumber { get; set; } = null!;
        public string Method { get; set; } = null!;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "EGP";

        public string CustomerFirstName { get; set; } = "Test";

        public string CustomerLastName { get; set; } = "User";

        public string CustomerEmail { get; set; } = "test@example.com";

        public string CustomerPhoneNumber { get; set; } = "+201000000000";
    }
}
