using AutoMapper;
using MediatR;
using Pos.tenant.Application.Exceptions;
using Pos.tenant.Application.Features.Tenants.DTOS;
using Pos.tenant.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Queries.GetTenantUsageCounters
{
    public class GetTenantUsageQuery:IRequest<TenantUsageCountersDto>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantUsageQueryHandler : IRequestHandler<GetTenantUsageQuery, TenantUsageCountersDto>
    {
        private readonly ITenantUsageCountersRepositoryAsync _tenantUsageCountersRepository;
        private readonly IMapper _mapper;

        public GetTenantUsageQueryHandler(ITenantUsageCountersRepositoryAsync tenantUsageCountersRepository,IMapper mapper)
        {
            _tenantUsageCountersRepository = tenantUsageCountersRepository;
            _mapper = mapper;
        }
        public async Task<TenantUsageCountersDto> Handle(GetTenantUsageQuery request, CancellationToken cancellationToken)
        {
            var tenantUsage=await _tenantUsageCountersRepository.GetByIdAsync(request.TenantId);

            if (tenantUsage == null)
                throw new ApiException($"Tenant usage counters with Tenant ID {request.TenantId} not found.");

            return _mapper.Map<TenantUsageCountersDto>(tenantUsage);
        }
    }
}
