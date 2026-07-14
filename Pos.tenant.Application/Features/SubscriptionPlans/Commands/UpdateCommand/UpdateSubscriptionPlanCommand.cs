using MediatR;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPlans.Commands.UpdateCommand
{
    public class UpdateSubscriptionPlanCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;

        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }

        public decimal MonthlyPrice { get; set; }

        public string CurrencyCode { get; set; } = "EGP";

        public int? BranchLimit { get; set; }
        public int? ProductLimit { get; set; }
        public int? CashierLimit { get; set; }

        public bool AllowVariants { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateSubscriptionPlanCommandHandler
        : IRequestHandler<UpdateSubscriptionPlanCommand, Result<Guid>>
    {
        private readonly ISubscriptionPlanRepositoryAsync _subscriptionPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSubscriptionPlanCommandHandler(
            ISubscriptionPlanRepositoryAsync subscriptionPlanRepository,
            IUnitOfWork unitOfWork)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            UpdateSubscriptionPlanCommand request,
            CancellationToken cancellationToken)
        {
            var subscriptionPlan = await _subscriptionPlanRepository.GetByIdAsync(request.Id);

            if (subscriptionPlan == null)
                return Result<Guid>.Failure("Subscription plan not found.");

            var normalizedCode = request.Code.Trim().ToUpperInvariant();
            var normalizedCurrencyCode = request.CurrencyCode.Trim().ToUpperInvariant();

            if (subscriptionPlan.Code != normalizedCode)
            {
                var codeExists = await _subscriptionPlanRepository.IsCodeExistsAsync(normalizedCode);

                if (codeExists)
                    return Result<Guid>.Failure("Subscription plan code already exists.");
            }

            subscriptionPlan.Code = normalizedCode;

            subscriptionPlan.NameAr = request.NameAr.Trim();
            subscriptionPlan.NameEn = request.NameEn.Trim();

            subscriptionPlan.DescriptionAr = request.DescriptionAr?.Trim();
            subscriptionPlan.DescriptionEn = request.DescriptionEn?.Trim();

            subscriptionPlan.MonthlyPrice = request.MonthlyPrice;
            subscriptionPlan.CurrencyCode = normalizedCurrencyCode;

            subscriptionPlan.BranchLimit = request.BranchLimit;
            subscriptionPlan.ProductLimit = request.ProductLimit;
            subscriptionPlan.CashierLimit = request.CashierLimit;

            subscriptionPlan.AllowVariants = request.AllowVariants;
            subscriptionPlan.IsActive = request.IsActive;

            _subscriptionPlanRepository.Update(subscriptionPlan);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(subscriptionPlan.Id);
        }
    }
}
