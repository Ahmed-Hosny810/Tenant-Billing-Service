using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPayments.Commands.CheckoutCommand
{
    public class StartPaymobCheckoutCommandValidator:AbstractValidator<StartPaymobCheckoutCommand>
    {
        public StartPaymobCheckoutCommandValidator()
        {
            RuleFor(x => x.InvoiceId)
                .NotEmpty();

            RuleFor(x => x.IdempotencyKey)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
