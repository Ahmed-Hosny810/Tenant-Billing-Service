using AutoMapper;
using MediatR;
using Pos.tenant.Application.Features.SubscriptionPlans.DTOs;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPlans.Queries.GetByIdQuery
{
    public class GetSubscriptionPlanByIdQuery : IRequest<SubscriptionPlanDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetSubscriptionPlanByIdQueryHandler
        : IRequestHandler<GetSubscriptionPlanByIdQuery, SubscriptionPlanDto?>
    {
        private readonly ISubscriptionPlanRepositoryAsync _subscriptionPlanRepository;
        private readonly IMapper _mapper;

        public GetSubscriptionPlanByIdQueryHandler(
            ISubscriptionPlanRepositoryAsync subscriptionPlanRepository,
            IMapper mapper)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _mapper = mapper;
        }

        public async Task<SubscriptionPlanDto?> Handle(
            GetSubscriptionPlanByIdQuery request,
            CancellationToken cancellationToken)
        {
            var subscriptionPlan = await _subscriptionPlanRepository.GetByIdAsync(request.Id);

            if (subscriptionPlan == null)
                return null;

            return _mapper.Map<SubscriptionPlanDto>(subscriptionPlan);
        }
    }
}
