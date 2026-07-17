using AutoMapper;
using MediatR;
using Pos.tenant.Application.Features.Tenants.DTOS;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.Tenants.Queries.GetAllQuery
{
    public class GetAllTenantsQuery: IRequest<PagedResponse<IEnumerable<TenantDto>>>
    {
        public GetAllTenantsQueryParameter Parameter { get; set; } = new();
    }
    public class GetAllTenantsQueryHandler
        : IRequestHandler<GetAllTenantsQuery, PagedResponse<IEnumerable<TenantDto>>>
    {
        private readonly ITenantRepositoryAsync _tenantRepository;
        private readonly IMapper _mapper;

        public GetAllTenantsQueryHandler(ITenantRepositoryAsync tenantRepository,IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<TenantDto>>> Handle(
            GetAllTenantsQuery request,
            CancellationToken cancellationToken)
        {
            

            var pagedTenants = await _tenantRepository.GetTenantsPagedResponseAsync(
                request.Parameter.Filter,
                request.Parameter.Includes,
                request.Parameter.OrderKey,
                request.Parameter.OrderDescending,
                request.Parameter.PageNumber,
                request.Parameter.PageSize);

             var tenantDtos = _mapper.Map<IEnumerable<TenantDto>>(pagedTenants.Data);

            return new PagedResponse<IEnumerable<TenantDto>>(
                tenantDtos,
                pagedTenants.PageNumber,
                pagedTenants.PageSize,
                pagedTenants.TotalCount);
        }
    }
}
