using MediatR;
using Pos.tenant.Application.Features.SubscriptionPayments.DTOS;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Interfaces.Services;
using Pos.tenant.Application.Wrappers;
using Pos.tenant.Domain.Constants;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPayments.Commands.CheckoutCommand
{
    public class StartPaymobCheckoutCommand : IRequest<Result<PaymobCheckoutDto>>
    {
        public Guid InvoiceId { get; set; }

        public string IdempotencyKey { get; set; } = null!;
    }
    public class StartPaymobCheckoutCommandHandler: IRequestHandler<StartPaymobCheckoutCommand, Result<PaymobCheckoutDto>>
    {
        private readonly ISubscriptionInvoiceRepositoryAsync _subscriptionInvoiceRepository;
        private readonly ISubscriptionPaymentRepositoryAsync _subscriptionPaymentRepository;
        private readonly IPaymobPaymentService _paymobPaymentService;
        private readonly IUnitOfWork _unitOfWork;

        public StartPaymobCheckoutCommandHandler(
            ISubscriptionInvoiceRepositoryAsync subscriptionInvoiceRepository,
            ISubscriptionPaymentRepositoryAsync subscriptionPaymentRepository,
            IPaymobPaymentService paymobPaymentService,
            IUnitOfWork unitOfWork)
        {
            _subscriptionInvoiceRepository = subscriptionInvoiceRepository;
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _paymobPaymentService = paymobPaymentService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaymobCheckoutDto>> Handle(StartPaymobCheckoutCommand request, CancellationToken cancellationToken)
        {
            var idempotencyKey = request.IdempotencyKey.Trim();

            var existingPayment = await _subscriptionPaymentRepository.GetByIdempotencyKeyAsync(idempotencyKey, cancellationToken);

            if (existingPayment != null)
            {
                if (existingPayment.InvoiceId!=request.InvoiceId)
                {
                    return Result<PaymobCheckoutDto>.Failure("This idempotency key was already used for a different invoice.");
                }

                if (string.IsNullOrWhiteSpace(existingPayment.ProviderClientSecret))
                {
                    return Result<PaymobCheckoutDto>.Failure("Checkout is already being processed. Please try again shortly.");
                }

                var existingCheckoutUrl= _paymobPaymentService.BuildCheckoutUrl(existingPayment.ProviderClientSecret);

                return Result<PaymobCheckoutDto>.Success(new PaymobCheckoutDto
                {
                    PaymentId = existingPayment.Id,
                    InvoiceId = existingPayment.InvoiceId,
                    Provider = existingPayment.Provider!,
                    Status = existingPayment.Status,
                    CheckoutUrl = existingCheckoutUrl
                });
            }
            var invoice = await _subscriptionInvoiceRepository.GetByIdAsync(request.InvoiceId);

            if (invoice == null)
                return Result<PaymobCheckoutDto>.Failure("Subscription invoice not found.");

            if (invoice.Status == InvoiceStatuses.Cancelled)
                return Result<PaymobCheckoutDto>.Failure("Cannot start checkout for a cancelled invoice.");

            if (invoice.Status == InvoiceStatuses.Paid)
                return Result<PaymobCheckoutDto>.Failure("Invoice is already paid.");

            var payment = new SubscriptionPayment
            {
                Id = Guid.NewGuid(),
                TenantId = invoice.TenantId,
                InvoiceId = invoice.Id,
                Amount = invoice.Total,
                Method = PaymentMethods.OnlineCard,
                Provider = PaymentProviders.Paymob,
                Status = PaymentStatuses.Pending,
                PaidAt = null,
                IdempotencyKey = idempotencyKey
            };

            await _subscriptionPaymentRepository.AddAsync(payment);

            var paymobResult = await _paymobPaymentService.CreateIntentionAsync(
                new PaymobCreateIntentionRequest
                {
                    PaymentId = payment.Id,
                    InvoiceId = invoice.InvoiceNumber,
                    InvoiceNumber = invoice.InvoiceNumber,
                    Amount = invoice.Total,
                    Currency = "EGP"
                },
                cancellationToken);

            payment.ProviderClientSecret = paymobResult.ClientSecret;
            payment.ProviderPaymentReference = paymobResult.ProviderPaymentReference;
            payment.ProviderStatus = paymobResult.ProviderStatus ?? "intended";

            _subscriptionPaymentRepository.Update(payment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<PaymobCheckoutDto>.Success(new PaymobCheckoutDto
            {
                PaymentId = payment.Id,
                InvoiceId = payment.InvoiceId,
                Provider = payment.Provider,
                Status = payment.Status,
                CheckoutUrl = paymobResult.CheckoutUrl
            });

        }
    }
}
