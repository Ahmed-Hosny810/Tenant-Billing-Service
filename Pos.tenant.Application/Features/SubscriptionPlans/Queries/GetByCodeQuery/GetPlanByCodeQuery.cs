using AutoMapper;
using MediatR;
using Pos.tenant.Application.Features.SubscriptionPlans.DTOs;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPlans.Queries.GetByCodeQuery
{
    public class GetPlanByCodeQuery:IRequest<SubscriptionPlanDto?>
    {
        public string Code { get; set; } = null!;
    }
    public class GetPlanByCodeQueryHandler : IRequestHandler<GetPlanByCodeQuery, SubscriptionPlanDto?>
    {
        private readonly ISubscriptionPlanRepositoryAsync _subscriptionPlanRepository;
        private readonly IMapper _mapper;

        public GetPlanByCodeQueryHandler(ISubscriptionPlanRepositoryAsync subscriptionPlanRepository,IMapper mapper)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _mapper = mapper;
        }
        public async Task<SubscriptionPlanDto?> Handle(GetPlanByCodeQuery request, CancellationToken cancellationToken)
        {
            var code = request.Code.Trim().ToUpperInvariant();

            var plan = await _subscriptionPlanRepository.GetByCodeAsync(code);

            if (plan == null)
                return null;

            return _mapper.Map<SubscriptionPlanDto>(plan);
        }
    }
}
