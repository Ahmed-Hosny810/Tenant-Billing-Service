using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Commands.StatusCommand.Validators
{
    public class ActivateTenantCommandValidator : AbstractValidator<ActivateTenantCommand>
    {
        public ActivateTenantCommandValidator()
        {
            RuleFor(x => x.TenantId)
                .NotEmpty();

            RuleFor(x => x.Reason)
                .MaximumLength(500);
        }
    }
}
