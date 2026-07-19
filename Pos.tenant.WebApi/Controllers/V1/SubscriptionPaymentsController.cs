using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pos.tenant.Application.Features.SubscriptionPayments.Commands.CreateCommand;
using Pos.tenant.Application.Features.SubscriptionPayments.DTOS;
using Pos.tenant.Application.Wrappers;

namespace Pos.tenant.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/payments")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SubscriptionPaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionPaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // for testing purposes, this endpoint allows manual registration of subscription payments
        [HttpPost("manual")]
        public async Task<ActionResult<Response<SubscriptionPaymentDto>>> RegisterPayment([FromBody] RegisterSubscriptionPaymentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new Response<SubscriptionPaymentDto>
                {
                    Succeeded = false,
                    Message = "Subscription payment registration failed.",
                    Errors = result.Errors
                });
            }

            return Ok(
                new Response<SubscriptionPaymentDto>(
                    result.Value,
                    "Subscription payment registered successfully."
                ));
        }


    }
}
