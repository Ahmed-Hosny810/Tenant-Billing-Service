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

        public GetAllTenantsQueryHandler(ITenantRepositoryAsync tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<PagedResponse<IEnumerable<TenantDto>>> Handle(
            GetAllTenantsQuery request,
            CancellationToken cancellationToken)
        {
            var parameter = request.Parameter;

            var pagedTenants = await _tenantRepository.GetTenantsPagedResponseAsync(
                parameter.Filter,
                parameter.Includes,
                parameter.OrderKey,
                parameter.OrderDescending,
                parameter.PageNumber,
                parameter.PageSize);

            var tenantDtos = pagedTenants.Data
                .Select(x => new TenantDto(x, parameter.Includes))
                .ToList();

            return new PagedResponse<IEnumerable<TenantDto>>(
                tenantDtos,
                pagedTenants.PageNumber,
                pagedTenants.PageSize,
                pagedTenants.TotalCount);
        }
    }
}
