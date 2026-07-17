using AutoMapper;
using MediatR;
using Pos.tenant.Application.Features.Tenants.DTOS;
using Pos.tenant.Application.Features.Tenants.Queries.GetAllQuery;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Queries.GetByIdQuery
{
    public class GetTenantByIdQuery: IRequest<Response<TenantDto>>
    {
        public Guid TenantId { get; set; }
        public TenantIncludes Includes { get; set; } = new();
    }
    
    public class GetTenantByIdQueryHandler: IRequestHandler<GetTenantByIdQuery, Response<TenantDto>>
    {
        private readonly ITenantRepositoryAsync _tenantRepository;
        private readonly IMapper _mapper;
        public GetTenantByIdQueryHandler(ITenantRepositoryAsync tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }
        public async Task<Response<TenantDto>> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(request.TenantId, request.Includes);
            if (tenant == null)
            {
                return new Response<TenantDto>(null, "Tenant not found");
            }
            var tenantDto = _mapper.Map<TenantDto>(tenant);
            return new Response<TenantDto>(tenantDto);
        }
    }
}
