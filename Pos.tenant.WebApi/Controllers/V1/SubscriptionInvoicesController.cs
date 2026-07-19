using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pos.tenant.Application.Features.SubscriptionInvoices.Commands.CreateCommand;
using Pos.tenant.Application.Features.SubscriptionInvoices.DTOS;
using Pos.tenant.Application.Features.SubscriptionInvoices.Queries.GetAllQuery;
using Pos.tenant.Application.Features.SubscriptionInvoices.Queries.GetByIdQuery;
using Pos.tenant.Application.Wrappers;

namespace Pos.tenant.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/tenant-invoices")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SubscriptionInvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionInvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Response<SubscriptionInvoiceDto>>> Create(Guid tenantId,[FromBody] CreateSubscriptionInvoiceCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new Response<SubscriptionInvoiceDto>
                {
                    Succeeded = false,
                    Message = "Subscription invoice creation failed.",
                    Errors = result.Errors
                });
            }

            return Ok( new Response<SubscriptionInvoiceDto>(result.Value,"Subscription invoice created successfully."));
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<IEnumerable<SubscriptionInvoiceDto>>>> GetTenantInvoices(
           Guid tenantId,
           [FromQuery] GetAllTenantInvoicesQueryParameter parameter)
        { 
            return Ok(await _mediator.Send(new GetAllTenantInvoicesQuery {TenantId = tenantId, Parameter = parameter} ) );
        }

        [HttpGet("{invoiceId:guid}")]
        public async Task<ActionResult<SubscriptionInvoiceDto>> GetTenantInvoiceById( Guid invoiceId)
        {
            return Ok(await _mediator.Send(new GetInvoiceByIdQuery { InvoiceId = invoiceId }));
        }
    }
}
