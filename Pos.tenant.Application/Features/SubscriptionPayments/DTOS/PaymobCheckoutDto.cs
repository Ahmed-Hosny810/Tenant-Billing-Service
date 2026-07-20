using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPayments.DTOS
{
    public class PaymobCheckoutDto
    {
        public Guid PaymentId { get; set; }

        public Guid InvoiceId { get; set; }

        public string Provider { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string CheckoutUrl { get; set; } = null!;
    }
}
