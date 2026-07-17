using MediatR;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using Pos.tenant.Domain.Constants;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Commands.StatusCommand
{
    public class ActivateTenantCommand : IRequest<Result<Guid>>
    {
        public Guid TenantId { get; set; }
        public string? Reason { get; set; }
    }
    public class ActivateTenantCommandHandler : IRequestHandler<ActivateTenantCommand, Result<Guid>>
    {
        private readonly ITenantRepositoryAsync _tenantRepository;
        private readonly ITenantStatusHistoryRepositoryAsync _tenantStatusHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateTenantCommandHandler(ITenantRepositoryAsync tenantRepository,ITenantStatusHistoryRepositoryAsync tenantStatusHistoryRepository,IUnitOfWork unitOfWork)
        {
            _tenantRepository = tenantRepository;
            _tenantStatusHistoryRepository = tenantStatusHistoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(ActivateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant=await _tenantRepository.GetByIdAsync(request.TenantId);

            if (tenant == null)
                return Result<Guid>.Failure("Tenant not found.");

            if (tenant.Status == TenantStatuses.Active)
                return Result<Guid>.Failure($"Tenant is already {tenant.Status}.");

            if (tenant.Status == TenantStatuses.Cancelled)
                return Result<Guid>.Failure("Cancelled tenant status cannot be changed.");

            var oldStatus = tenant.Status;
            
            tenant.Status = TenantStatuses.Active;
             _tenantRepository.Update(tenant);

            var statusHistory = new TenantStatusHistory
            {
                TenantId = tenant.Id,
                OldStatus = oldStatus,
                NewStatus = TenantStatuses.Active,
                Reason = request.Reason,
                ChangedAt = DateTime.UtcNow
            };
            await _tenantStatusHistoryRepository.AddAsync(statusHistory);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(tenant.Id);
        }
    }
}
