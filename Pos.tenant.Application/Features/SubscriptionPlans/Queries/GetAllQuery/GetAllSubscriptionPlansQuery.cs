using AutoMapper;
using MediatR;
using Pos.tenant.Application.Features.SubscriptionPlans.DTOs;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;


namespace Pos.tenant.Application.Features.SubscriptionPlans.Queries.GetAllQuery
{
    public class GetAllSubscriptionPlansQuery:IRequest<IEnumerable<SubscriptionPlanDto>>
    {
    }
    public class GetAllSubscriptionPlansQueryHandler : IRequestHandler<GetAllSubscriptionPlansQuery, IEnumerable<SubscriptionPlanDto>>
    {
        private readonly ISubscriptionPlanRepositoryAsync _subscriptionPlanRepository;
        private readonly IMapper _mapper;

        public GetAllSubscriptionPlansQueryHandler(ISubscriptionPlanRepositoryAsync subscriptionPlanRepository,IMapper mapper)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SubscriptionPlanDto>> Handle(GetAllSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            var subscriptionPlans = await _subscriptionPlanRepository.GetAllAsync();
            var subscriptionPlansDto=_mapper.Map<IEnumerable<SubscriptionPlanDto>>(subscriptionPlans);
            return subscriptionPlansDto;
        }
    }

}
