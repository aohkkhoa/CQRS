using Application.Features.OrderFeatures.Commands;
using Application.Features.OrderFeatures.Queries;
using Domain.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrapper;
using IResult = Shared.Wrapper.IResult;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        public OrderController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{customerId}")]
        public async Task<Result<IEnumerable<OrderInformation>>> GetAllOrderInformation(int customerId)
        {
            return await _mediator.Send(new GetAllOrderInformationQuery { CustomerId = customerId });
        }

        [HttpGet("total/{customerId}")]
        public async Task<IResult<float>> TotalWillPay(int customerId)
        {
            return await _mediator.Send(new GetTotalPriceWillPayQuery { CustomerId = customerId });
        }

        [HttpGet("orderWillPay/{customerId}")]
        public async Task<IResult<IEnumerable<OrderInformation>>> OrderBeforePay(int customerId)
        {
            return await _mediator.Send(new GetOrderWillPayQuery { CustomerId = customerId });
        }

        [HttpPut("paid/{customerId}")]
        public async Task<IResult> Paid(int customerId)
        {
            return await _mediator.Send(new UpdateCheckPaidOrderCommand { CustomerId = customerId });
        }
    }
}