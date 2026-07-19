using MediatR;
using Pos.tenant.Application.Features.Tenants.DTOS;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using Pos.tenant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Commands.TenantUsageCountersCommands
{
    public class IncreaseTenantUsageCommand : IRequest<Result<Guid>>
    {
        public Guid TenantId { get; set; }
        public TenantUsageCounterType CounterType { get; set; }
    }

    public class IncreaseTenantUsageCommandHandler : IRequestHandler<IncreaseTenantUsageCommand, Result<Guid>>
    {
        private readonly ITenantUsageCountersRepositoryAsync _tenantUsageCountersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantSubscriptionRepositoryAsync _tenantSubscriptionRepository;

        public IncreaseTenantUsageCommandHandler(ITenantUsageCountersRepositoryAsync tenantUsageCountersRepository,IUnitOfWork unitOfWork,
            ITenantSubscriptionRepositoryAsync tenantSubscriptionRepository)
        {
            _tenantUsageCountersRepository = tenantUsageCountersRepository;
            _unitOfWork = unitOfWork;
            _tenantSubscriptionRepository = tenantSubscriptionRepository;
        }
        public async Task<Result<Guid>> Handle(IncreaseTenantUsageCommand request, CancellationToken cancellationToken)
        {
            var tenantUsageCounter = await _tenantUsageCountersRepository.GetByIdAsync(request.TenantId);

            if (tenantUsageCounter == null)
            {
                return Result<Guid>.Failure($"Tenant usage counters with Tenant ID {request.TenantId} not found.");
            }

            var subscription = await _tenantSubscriptionRepository
                .GetCurrentPlanByTenantIdAsync(request.TenantId);

            if (subscription == null)
            {
                return Result<Guid>.Failure($"Tenant subscription with Tenant ID {request.TenantId} not found.");
            }

            if (subscription.Plan == null)
            {
                return Result<Guid>.Failure("Subscription plan not found.");
            }

            var plan = subscription.Plan;

            switch (request.CounterType)
            {
                case TenantUsageCounterType.Branch:
                    if (plan.BranchLimit.HasValue && tenantUsageCounter.BranchCount >= plan.BranchLimit.Value)
                    { 
                        return Result<Guid>.Failure($"Branch limit exceeded. Current limit is {plan.BranchLimit.Value}.");
                    }
                    tenantUsageCounter.BranchCount++;
                    break;
                case TenantUsageCounterType.Product:
                    if (plan.ProductLimit.HasValue &&
                        tenantUsageCounter.ProductCount >= plan.ProductLimit.Value)
                    {
                        return Result<Guid>.Failure(
                            $"Product limit exceeded. Current limit is {plan.ProductLimit.Value}.");
                    }

                    tenantUsageCounter.ProductCount++;
                    break;

                case TenantUsageCounterType.Cashier:
                    if (plan.CashierLimit.HasValue &&
                        tenantUsageCounter.CashierCount >= plan.CashierLimit.Value)
                    {
                        return Result<Guid>.Failure(
                            $"Cashier limit exceeded. Current limit is {plan.CashierLimit.Value}.");
                    }

                    tenantUsageCounter.CashierCount++;
                    break;

                default:
                    return Result<Guid>.Failure($"Invalid counter type: {request.CounterType}");
            }

            tenantUsageCounter.UpdatedAt = DateTime.UtcNow;

            _tenantUsageCountersRepository.Update(tenantUsageCounter);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

             return Result<Guid>.Success(tenantUsageCounter.TenantId);
        }
    }
}
