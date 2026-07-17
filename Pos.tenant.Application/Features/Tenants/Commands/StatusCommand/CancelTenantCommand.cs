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
    public class CancelTenantCommand : IRequest<Result<Guid>>
    {
        public Guid TenantId { get; set; }
        public string? Reason { get; set; }
    }
    public class CancelTenantCommandHandler : IRequestHandler<CancelTenantCommand, Result<Guid>>
    {
        private readonly ITenantRepositoryAsync _tenantRepository;
        private readonly ITenantStatusHistoryRepositoryAsync _tenantStatusHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelTenantCommandHandler(ITenantRepositoryAsync tenantRepository, ITenantStatusHistoryRepositoryAsync tenantStatusHistoryRepository,IUnitOfWork unitOfWork )
        {
            _tenantRepository = tenantRepository;
            _tenantStatusHistoryRepository = tenantStatusHistoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CancelTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
            if (tenant == null)
                return Result<Guid>.Failure("Tenant not found.");

            if (tenant.Status == TenantStatuses.Cancelled)
                return Result<Guid>.Failure($"Tenant is already {tenant.Status}.");

            var oldStatus = tenant.Status;
            tenant.Status = TenantStatuses.Cancelled;
            _tenantRepository.Update(tenant);
            var statusHistory = new TenantStatusHistory
            {
                TenantId = tenant.Id,
                OldStatus = oldStatus,
                NewStatus = TenantStatuses.Cancelled,
                Reason = request.Reason,
                ChangedAt = DateTime.UtcNow
            };
            await _tenantStatusHistoryRepository.AddAsync(statusHistory);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(tenant.Id);
        }
    }
}
