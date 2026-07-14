using MediatR;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPlans.Commands.DeleteCommand
{
    public class DeleteSubscriptionPlanCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSubscriptionPlanCommandHandler
        : IRequestHandler<DeleteSubscriptionPlanCommand, Result<Guid>>
    {
        private readonly ISubscriptionPlanRepositoryAsync _subscriptionPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubscriptionPlanCommandHandler(
            ISubscriptionPlanRepositoryAsync subscriptionPlanRepository,
            IUnitOfWork unitOfWork)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(
            DeleteSubscriptionPlanCommand request,
            CancellationToken cancellationToken)
        {
            var subscriptionPlan = await _subscriptionPlanRepository.GetByIdAsync(request.Id);

            if (subscriptionPlan == null)
                return Result<Guid>.Failure("Subscription plan not found.");

            if (!subscriptionPlan.IsActive)
                return Result<Guid>.Failure("Subscription plan is already inactive.");

            subscriptionPlan.IsActive = false;

            _subscriptionPlanRepository.Update(subscriptionPlan);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(subscriptionPlan.Id);
        }
    }
}
