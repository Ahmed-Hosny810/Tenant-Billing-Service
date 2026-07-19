using MediatR;
using Pos.tenant.Application.Features.SubscriptionPayments.DTOS;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using Pos.tenant.Domain.Constants;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionPayments.Commands.CreateCommand
{
    public class RegisterSubscriptionPaymentCommand: IRequest<Result<SubscriptionPaymentDto>>
    {
        public Guid InvoiceId { get; set; }

        public decimal Amount { get; set; }

        public string Method { get; set; } = null!;

        public string? ReferenceNumber { get; set; }

        public bool MarkAsCompleted { get; set; }
    }
    public class RegisterSubscriptionPaymentCommandHandler : IRequestHandler<RegisterSubscriptionPaymentCommand, Result<SubscriptionPaymentDto>>
    {
        private readonly ISubscriptionPaymentRepositoryAsync _subscriptionPaymentRepository;
        private readonly ISubscriptionInvoiceRepositoryAsync _subscriptionInvoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterSubscriptionPaymentCommandHandler(ISubscriptionPaymentRepositoryAsync subscriptionPaymentRepository,
                                 ISubscriptionInvoiceRepositoryAsync subscriptionInvoiceRepository,IUnitOfWork unitOfWork)
        {
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _subscriptionInvoiceRepository = subscriptionInvoiceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<SubscriptionPaymentDto>> Handle(RegisterSubscriptionPaymentCommand request, CancellationToken cancellationToken)
        {
            var invoice = await  _subscriptionInvoiceRepository.GetByIdAsync(request.InvoiceId);

            if (invoice == null) 
                return Result<SubscriptionPaymentDto>.Failure("Invoice not found");

            if (invoice.Status== "Paid")
                return Result<SubscriptionPaymentDto>.Failure("Invoice is already paid");

            if (invoice.Status == InvoiceStatuses.Cancelled)
                return Result<SubscriptionPaymentDto>.Failure("Cannot pay a cancelled invoice.");

            if (request.MarkAsCompleted && request.Amount!=invoice.Total)
            {
                return Result<SubscriptionPaymentDto>.Failure("Completed payment amount must equal invoice total.");
            }

            var now = DateTime.UtcNow;

            var payment = new SubscriptionPayment
            {
                Id = Guid.NewGuid(),
                TenantId = invoice.TenantId,
                InvoiceId = invoice.Id,
                Amount = request.Amount,
                Method = request.Method.Trim(),
                ReferenceNumber = string.IsNullOrWhiteSpace(request.ReferenceNumber) ? null: request.ReferenceNumber.Trim(),
                Status = request.MarkAsCompleted ? PaymentStatuses.Completed: PaymentStatuses.Pending,
                PaidAt = request.MarkAsCompleted ? now : null
            };

            if (request.MarkAsCompleted)
            {
                invoice.MarkPaid(now);
                _subscriptionInvoiceRepository.Update(invoice);
            }

            await _subscriptionPaymentRepository.AddAsync(payment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new SubscriptionPaymentDto
            {
                PaymentId = payment.Id,
                TenantId = payment.TenantId,
                InvoiceId = payment.InvoiceId,
                Amount = payment.Amount,
                Method = payment.Method,
                ReferenceNumber = payment.ReferenceNumber,
                Status = payment.Status,
                PaidAt = payment.PaidAt
            };

            return Result<SubscriptionPaymentDto>.Success(dto);
        }
    }
}
