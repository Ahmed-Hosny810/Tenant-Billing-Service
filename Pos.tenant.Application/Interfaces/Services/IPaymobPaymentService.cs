using Pos.tenant.Application.Features.SubscriptionPayments.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Interfaces.Services
{
    public interface IPaymobPaymentService
    {
        Task<PaymobCreateIntentionResult> CreateIntentionAsync( PaymobCreateIntentionRequest request,CancellationToken cancellationToken);

        string BuildCheckoutUrl(string clientSecret);
    }
}
