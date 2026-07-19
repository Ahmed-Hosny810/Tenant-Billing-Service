using AutoMapper;
using MediatR;
using Pos.tenant.Application.Exceptions;
using Pos.tenant.Application.Features.Tenants.DTOS;
using Pos.tenant.Application.Interfaces.Repositories;

namespace Pos.tenant.Application.Features.Tenants.Queries.GetTenantSettingsQuery
{
    public class GetTenantSettingsByIdQuery:IRequest<TenantSettingsDto>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantSettingsByIdQueryHandler : IRequestHandler<GetTenantSettingsByIdQuery, TenantSettingsDto>
    {
        private readonly ITenantSettingsRepositoryAsync _tenantSettingsRepository;
        private readonly IMapper _mapper;

        public GetTenantSettingsByIdQueryHandler(ITenantSettingsRepositoryAsync tenantSettingsRepository,IMapper mapper)
        {
            _tenantSettingsRepository = tenantSettingsRepository;
            _mapper = mapper;
        }
        public async Task<TenantSettingsDto> Handle(GetTenantSettingsByIdQuery request, CancellationToken cancellationToken)
        {
            var tenantSettings = await _tenantSettingsRepository.GetByIdAsync(request.TenantId);

            if (tenantSettings == null) 
                throw new ApiException ($"Tenant settings not found for TenantId: {request.TenantId}");

            return _mapper.Map<TenantSettingsDto>(tenantSettings);

        }
    }
}
