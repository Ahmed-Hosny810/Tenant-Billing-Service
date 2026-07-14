using AutoMapper;
using MediatR;
using Pos.tenant.Application.Features.SubscriptionPlans.DTOs;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPlans.Queries.GetAllActivePlansQuery
{
    public class GetActivePlansQuery : IRequest<IEnumerable<SubscriptionPlanDto>>
    {
    }
    public class GetActivePlansQueryHandler : IRequestHandler<GetActivePlansQuery, IEnumerable<SubscriptionPlanDto>>
    {
        private readonly ISubscriptionPlanRepositoryAsync _subscriptionPlanRepository;
        private readonly IMapper _mapper;
        public GetActivePlansQueryHandler(ISubscriptionPlanRepositoryAsync subscriptionPlanRepository, IMapper mapper)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SubscriptionPlanDto>> Handle(GetActivePlansQuery request, CancellationToken cancellationToken)
        {
            var activePlans = await _subscriptionPlanRepository.GetActivePlansAsync();
            var activePlansDto = _mapper.Map<IEnumerable<SubscriptionPlanDto>>(activePlans);
            return activePlansDto;
        }
    }
}
