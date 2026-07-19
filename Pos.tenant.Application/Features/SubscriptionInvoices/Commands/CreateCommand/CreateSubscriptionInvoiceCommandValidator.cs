using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionInvoices.Commands.CreateCommand
{
    public class CreateSubscriptionInvoiceCommandValidator
       : AbstractValidator<CreateSubscriptionInvoiceCommand>
    {
        public CreateSubscriptionInvoiceCommandValidator()
        {
            RuleFor(x => x.TenantId)
                .NotEmpty();

            RuleFor(x => x.PeriodStart)
                .NotEmpty();

            RuleFor(x => x.PeriodEnd)
                .NotEmpty()
                .GreaterThan(x => x.PeriodStart)
                .WithMessage("Period end must be greater than period start.");

            RuleFor(x => x.DueDate)
                .NotEmpty();
        }
    }
}
