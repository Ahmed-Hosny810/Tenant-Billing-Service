using AutoMapper;
using MediatR;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPlans.Commands.CreateCommand
{
    public class CreateSubscriptionPlanCommand:IRequest<Result<Guid>>
    {
        public string Code { get; set; } = null!;

        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;

        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }

        public decimal MonthlyPrice { get; set; }

        public string CurrencyCode { get; set; } = "EGP";

        public int? BranchLimit { get; set; }
        public int? ProductLimit { get; set; }
        public int? CashierLimit { get; set; }

        public bool AllowVariants { get; set; }
    }
    public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, Result<Guid>>
    {
        private readonly ISubscriptionPlanRepositoryAsync _subscriptionPlanRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubscriptionPlanCommandHandler(ISubscriptionPlanRepositoryAsync subscriptionPlanRepository, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            request.Code = request.Code.Trim().ToUpperInvariant();
            request.CurrencyCode = request.CurrencyCode.Trim().ToUpperInvariant();

            var codeExists= await _subscriptionPlanRepository.IsCodeExistsAsync(request.Code);

            if (codeExists)
                return Result<Guid>.Failure("Subscription plan code already exists.");

            var subscriptionPlan = _mapper.Map<SubscriptionPlan>(request);

            subscriptionPlan.Id = Guid.NewGuid();

            await _subscriptionPlanRepository.AddAsync(subscriptionPlan);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(subscriptionPlan.Id);


        }
    }
}
