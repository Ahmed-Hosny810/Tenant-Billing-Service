using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPayments.DTOS
{
    public class PaymobCreateIntentionResult
    {
        public string ClientSecret { get; set; } = null!;

        public string CheckoutUrl { get; set; } = null!;

        public string? ProviderPaymentReference { get; set; }

        public string? ProviderStatus { get; set; }
    }

}
