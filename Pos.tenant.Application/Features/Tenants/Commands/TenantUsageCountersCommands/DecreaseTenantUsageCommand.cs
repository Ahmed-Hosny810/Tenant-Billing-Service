using MediatR;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using Pos.tenant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Commands.TenantUsageCountersCommands
{
    public class DecreaseTenantUsageCommand : IRequest<Result<Guid>>
    {
        public Guid TenantId { get; set; }
        public TenantUsageCounterType CounterType { get; set; }
    }
 
    public class DecreaseTenantUsageCommandHandler : IRequestHandler<DecreaseTenantUsageCommand, Result<Guid>>
    {
        private readonly ITenantUsageCountersRepositoryAsync _tenantUsageCountersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DecreaseTenantUsageCommandHandler(ITenantUsageCountersRepositoryAsync tenantUsageCountersRepository, IUnitOfWork unitOfWork)
        {
            _tenantUsageCountersRepository = tenantUsageCountersRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(DecreaseTenantUsageCommand request, CancellationToken cancellationToken)
        {
            var tenantUsageCounter = await _tenantUsageCountersRepository.GetByIdAsync(request.TenantId);

            if (tenantUsageCounter == null)
            {
                return Result<Guid>.Failure($"Tenant usage counters with Tenant ID {request.TenantId} not found.");
            }

            switch (request.CounterType)
            {
                case TenantUsageCounterType.Branch:
                    if (tenantUsageCounter.BranchCount <= 0)
                    {
                        return Result<Guid>.Failure("Branch count cannot be less than zero.");
                    }
                    tenantUsageCounter.BranchCount--;
                    break;
                case TenantUsageCounterType.Product:
                    if (tenantUsageCounter.ProductCount <= 0)
                    {
                        return Result<Guid>.Failure("Product count cannot be less than zero.");
                    }

                    tenantUsageCounter.ProductCount--;
                    break;

                case TenantUsageCounterType.Cashier:
                    if (tenantUsageCounter.CashierCount <= 0)
                    {
                        return Result<Guid>.Failure("Cashier count cannot be less than zero.");
                    }

                    tenantUsageCounter.CashierCount--;
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
