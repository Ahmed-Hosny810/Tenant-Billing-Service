using AutoMapper;
using MediatR;
using Pos.tenant.Application.Exceptions;
using Pos.tenant.Application.Features.SubscriptionInvoices.DTOS;
using Pos.tenant.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionInvoices.Queries.GetByIdQuery
{
    public class GetInvoiceByIdQuery:IRequest<SubscriptionInvoiceDto>
    {
        public Guid InvoiceId { get; set; }
    }

    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, SubscriptionInvoiceDto>
    {
        private readonly ISubscriptionInvoiceRepositoryAsync _subscriptionInvoiceRepository;
        private readonly IMapper _mapper;

        public GetInvoiceByIdQueryHandler(ISubscriptionInvoiceRepositoryAsync subscriptionInvoiceRepository, IMapper mapper)
        {
            _subscriptionInvoiceRepository = subscriptionInvoiceRepository;
            _mapper = mapper;
        }
        public async Task<SubscriptionInvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _subscriptionInvoiceRepository.GetByIdAsync(request.InvoiceId);

            if (invoice == null)
                throw new ApiException("Invoice not found.");

            return _mapper.Map<SubscriptionInvoiceDto>(invoice);
        }
    }
}
