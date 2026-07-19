using AutoMapper;
using MediatR;
using Pos.tenant.Application.Features.SubscriptionInvoices.DTOS;
using Pos.tenant.Application.Features.Tenants.DTOS;
using Pos.tenant.Application.Interfaces.Repositories;
using Pos.tenant.Application.Wrappers;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Features.SubscriptionInvoices.Queries.GetAllQuery
{
    public class GetAllTenantInvoicesQuery: IRequest<PagedResponse<IEnumerable<SubscriptionInvoiceDto>>>
    {
        public Guid TenantId { get; set; }
        public GetAllTenantInvoicesQueryParameter Parameter { get; set; }
    }
    public class GetAllTenantInvoicesQueryHandler : IRequestHandler<GetAllTenantInvoicesQuery, PagedResponse<IEnumerable<SubscriptionInvoiceDto>>>
    {
        private readonly ISubscriptionInvoiceRepositoryAsync _subscriptionInvoiceRepository;
        private readonly IMapper _mapper;

        public GetAllTenantInvoicesQueryHandler(ISubscriptionInvoiceRepositoryAsync subscriptionInvoiceRepository,IMapper mapper)
        {
            _subscriptionInvoiceRepository = subscriptionInvoiceRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<SubscriptionInvoiceDto>>> Handle(GetAllTenantInvoicesQuery request, CancellationToken cancellationToken)
        {
            var result = await _subscriptionInvoiceRepository.GetInvoicesPagedResponseAsync(request.TenantId, request.Parameter.Filter, request.Parameter.OrderKey,
                                         request.Parameter.OrderDescending,request.Parameter.PageNumber, request.Parameter.PageSize);


            var invoicesDtos = _mapper.Map<IEnumerable<SubscriptionInvoiceDto>>(result.Data);

            return new PagedResponse<IEnumerable<SubscriptionInvoiceDto>>(invoicesDtos, result.PageNumber, result.PageSize, result.TotalCount);
        }
    }
}
