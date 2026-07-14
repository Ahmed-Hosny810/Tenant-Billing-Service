using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPlans.Commands.CreateCommand
{
    public class CreateSubscriptionPlanCommandValidator:AbstractValidator<CreateSubscriptionPlanCommand>
    {
        public CreateSubscriptionPlanCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(80)
                .Matches("^[A-Z0-9_]+$")
                .WithMessage("Code must contain only uppercase letters, numbers, and underscores.");

            RuleFor(x => x.NameAr)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.NameEn)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.DescriptionAr)
                .MaximumLength(500);

            RuleFor(x => x.DescriptionEn)
                .MaximumLength(500);

            RuleFor(x => x.MonthlyPrice)
                .GreaterThan(0);

            RuleFor(x => x.CurrencyCode)
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.BranchLimit)
                .GreaterThan(0)
                .When(x => x.BranchLimit.HasValue);

            RuleFor(x => x.ProductLimit)
                .GreaterThan(0)
                .When(x => x.ProductLimit.HasValue);

            RuleFor(x => x.CashierLimit)
                .GreaterThan(0)
                .When(x => x.CashierLimit.HasValue);
        }
    }
}
