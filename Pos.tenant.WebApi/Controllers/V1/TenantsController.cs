using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pos.tenant.Application.Features.SubscriptionPlans.Commands.CreateCommand;
using Pos.tenant.Application.Features.Tenants.Commands.CreateCommand;
using Pos.tenant.Application.Features.Tenants.Commands.StatusCommand;
using Pos.tenant.Application.Features.Tenants.DTOS;
using Pos.tenant.Application.Features.Tenants.Queries.GetAllQuery;
using Pos.tenant.Application.Features.Tenants.Queries.GetByIdQuery;
using Pos.tenant.Application.Features.Tenants.Queries.GetStatusHistoryQuery;
using Pos.tenant.Application.Wrappers;

namespace Pos.tenant.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Response<Guid>>> Post([FromBody] CreateTenantCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new Response<Guid>
                {
                    Succeeded = false,
                    Message = "Tenant creation failed.",
                    Errors = result.Errors
                });
            }


            return Ok(new Response<Guid>(
                result.Value,
                "Tenant created successfully"
            ));
        }
        [HttpPost("GetAll")]
        public async Task<ActionResult<PagedResponse<IEnumerable<TenantDto>>>> Get([FromBody] GetAllTenantsQueryParameter parameter)
        {
            return Ok(await _mediator.Send(new GetAllTenantsQuery { Parameter = parameter }));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<TenantDto>>> GetById(Guid id, [FromQuery] TenantIncludes includes)
        {
            return Ok(await _mediator.Send(new GetTenantByIdQuery { TenantId = id, Includes = includes }));
        }

        [HttpPost("{id:guid}/activate")]
        public async Task<ActionResult<Response<Guid>>> Activate( Guid id,[FromBody] ActivateTenantCommand command)
        {
            var result = await _mediator.Send(new ActivateTenantCommand
            {
                TenantId = id,
                Reason = command.Reason
            });

            if (result.IsFailure)
            {
                return BadRequest(new Response<Guid>
                {
                    Succeeded = false,
                    Message = "Tenant activation failed.",
                    Errors = result.Errors
                });
            }

            return Ok(new Response<Guid>(
                result.Value,
                "Tenant activated successfully."
            ));
        }

        [HttpPost("{id:guid}/suspend")]
        public async Task<ActionResult<Response<Guid>>> Suspend(
            Guid id,
            [FromBody] SuspendTenantCommand command)
        {
            var result = await _mediator.Send(new SuspendTenantCommand
            {
                TenantId = id,
                Reason = command.Reason
            });

            if (result.IsFailure)
            {
                return BadRequest(new Response<Guid>
                {
                    Succeeded = false,
                    Message = "Tenant suspension failed.",
                    Errors = result.Errors
                });
            }

            return Ok(new Response<Guid>(
                result.Value,
                "Tenant suspended successfully."
            ));
        }

        [HttpPost("{id:guid}/cancel")]
        public async Task<ActionResult<Response<Guid>>> Cancel(Guid id,[FromBody] CancelTenantCommand command)
        {
            var result = await _mediator.Send(new CancelTenantCommand
            {
                TenantId = id,
                Reason = command.Reason
            });

            if (result.IsFailure)
            {
                return BadRequest(new Response<Guid>
                {
                    Succeeded = false,
                    Message = "Tenant cancellation failed.",
                    Errors = result.Errors
                });
            }

            return Ok(new Response<Guid>(
                result.Value,
                "Tenant cancelled successfully."
            ));
        }

        [HttpGet("{id:guid}/tenant-status-history")]
        public async Task<ActionResult<Response<IEnumerable<TenantStatusHistoryDto>>>> GetStatusHistory(Guid id)
        {
            var history = await _mediator.Send(new GetTenantStatusHistoryQuery
            {
                TenantId = id
            });

            return Ok(new Response<IEnumerable<TenantStatusHistoryDto>>(
                history,
                "Tenant status history retrieved successfully."
            ));
        }
    }
}   