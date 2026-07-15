using FluentValidation;
using Pos.tenant.Application.Helpers;
using Pos.tenant.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Commands.CreateCommand
{
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator()
        {
            RuleFor(x => x.NameEn)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.NameAr)
                .MaximumLength(200);


            RuleFor(x => x.BusinessTypeCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(businessType =>
                {
                    var normalizedBusinessType = businessType.Trim().ToUpperInvariant();

                    return BusinessType.IsSupported(normalizedBusinessType);
                })
                .WithMessage("Invalid business type.");

            RuleFor(x => x.CurrencyCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(currencyCode => currencyCode.Trim().ToUpperInvariant().Length == 3)
                .WithMessage("Invalid currency code.");

            RuleFor(x => x.InventoryMode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(inventoryMode =>
                {
                    return inventoryMode.Equals(TenantInventoryModes.TrackStock, StringComparison.OrdinalIgnoreCase)
                        || inventoryMode.Equals(TenantInventoryModes.NoStockTracking, StringComparison.OrdinalIgnoreCase);
                })
                .WithMessage("Invalid inventory mode.");

            RuleFor(x => x.PlanCode)
                .NotEmpty()
                .MaximumLength(80);
        }
    }
}
